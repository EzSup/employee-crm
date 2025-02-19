import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { AuthContextType } from "../models/authModels";

const initialState: AuthContextType = {
	user: {
		isAuthenticated: false,
		fullName: null,
		username: null,
	},
	loginErrors: null,
	refreshTerm: null,
};

const authSlice = createSlice({
	name: "auth",
	initialState,
	reducers: {
		setUser: (
			state,
			action: PayloadAction<{ fullName: string; username: string }>
		) => {
			state.user.isAuthenticated = true;
			state.user.fullName = action.payload.fullName;
			state.user.username = action.payload.username;
		},
		clearUser: (state) => {
			state.user.isAuthenticated = false;
			state.user.fullName = null;
			state.user.username = null;
			state.loginErrors = null;
		},
		setErrors: (
			state,
			action: PayloadAction<{ errors: { [key: string]: string[] } }>
		) => {
			state.loginErrors = action.payload;
		},
		updateRefreshTerm: (state) => {
			const now = new Date();
			now.setDate(now.getDate() + 1);
			state.refreshTerm = now;
		},
		clearRefreshTerm: (state) => {
			state.refreshTerm = null;
		},
	},
});

export const {
	setUser,
	clearUser,
	setErrors,
	updateRefreshTerm,
	clearRefreshTerm,
} = authSlice.actions;
export default authSlice.reducer;
