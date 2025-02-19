import { NavigateFunction } from "react-router-dom";
import { GetDataResponse, LoginRequest } from "../models/authModels";
import {
	setUser,
	clearUser,
	setErrors,
	updateRefreshTerm,
} from "../slices/authSlice";
import { AppDispatch, showGlobalSnackbar } from "../store/store";
import axiosInstance from "./axiosInstance";

//Asks the server whether user is authenticated and if he is, asks his data to save it in redux.
export const getStatus = async (
	dispatch: AppDispatch,
	navigate: NavigateFunction
) => {
	await axiosInstance
		.get<GetDataResponse>("/api/auth/me")
		.then((response) => {
			dispatch(
				setUser({
					fullName: response.data.fullName,
					username: response.data.username,
				})
			);
			dispatch(updateRefreshTerm());
			navigate("/");
		})
		.catch(() => {
			dispatch(clearUser());
			navigate("/login");
		});
};

//Makes an attempt to log in user, saves incorrect fields errors to display
export const login = async (
	request: LoginRequest,
	dispatch: AppDispatch,
	navigate: NavigateFunction
) => {
	await axiosInstance
		.post("/api/auth/login", request)
		.then(async () => {
			getStatus(dispatch, navigate);
			dispatch(setErrors({ errors: {} }));
			showGlobalSnackbar("Successfully logged in!", "success");
		})
		.catch((error) => {
			showGlobalSnackbar(
				"An error occurred while trying to log in!",
				"warning"
			);
			dispatch(
				setErrors({
					errors: error?.response?.data?.errors,
				})
			);
		});
};

//Makes an attempt to log out user
export const logout = async (
	dispatch: AppDispatch,
	navigate: NavigateFunction
) => {
	try {
		await axiosInstance.delete("/api/auth/logout");
		showGlobalSnackbar("Successfully logged out!", "info");
	} catch (error) {
		console.error(error);
		showGlobalSnackbar("An error occurred while trying to log out!", "warning");
	} finally {
		await getStatus(dispatch, navigate);
		navigate("/login");
	}
};
