import { Alert, Snackbar } from "@mui/material";
import { hideSnackbar } from "../../slices/snackbarSlice";
import { RootState } from "../../store/store.ts";
import { useDispatch, useSelector } from "react-redux";
import { FC } from "react";

const GlobalSnackbar: FC = () => {
	const dispatch = useDispatch();
	const snackbarState = useSelector((state: RootState) => state.snackbar);
	const handleClose = () => dispatch(hideSnackbar());

	return (
		<Snackbar
			anchorOrigin={{ vertical: "top", horizontal: "right" }}
			open={snackbarState.open}
			autoHideDuration={3000}
			onClose={handleClose}
		>
			<Alert
				onClose={handleClose}
				severity={snackbarState.severity}
				variant="filled"
				sx={{ width: "100%" }}
			>
				{snackbarState.message}
			</Alert>
		</Snackbar>
	);
};

export default GlobalSnackbar;
