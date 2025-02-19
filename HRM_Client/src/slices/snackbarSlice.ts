import { createSlice } from "@reduxjs/toolkit";
import { SnackbarState } from "../models/snackbarModels";

const initialState: SnackbarState = {
	open: false,
	message: "",
	severity: "info",
};

const snackbarSlice = createSlice({
	name: "snackbar",
	initialState,
	reducers: {
		showSnackbar: (state, action) => {
			state.open = true;
			state.message = action.payload.message;
			state.severity = action.payload.severity || "info";
		},
		hideSnackbar: (state) => {
			state.open = false;
		},
	},
});

export const { showSnackbar, hideSnackbar } = snackbarSlice.actions;
export default snackbarSlice.reducer;
