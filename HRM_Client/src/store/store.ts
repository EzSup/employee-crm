import { configureStore } from "@reduxjs/toolkit";
import { persistStore, persistReducer } from "redux-persist";
import storage from "redux-persist/lib/storage";
import authReducer from "../slices/authSlice";
import snackbarReducer, { showSnackbar } from "../slices/snackbarSlice";

const persistConfig = {
	key: "auth",
	storage,
};

const persistedReducer = persistReducer(persistConfig, authReducer);

export const store = configureStore({
	reducer: {
		auth: persistedReducer,
		snackbar: snackbarReducer,
	},
});

export const persistor = persistStore(store);

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export const showGlobalSnackbar = (
	message: string,
	severity: "success" | "error" | "warning" | "info"
) => {
	store.dispatch(showSnackbar({ message, severity }));
};
