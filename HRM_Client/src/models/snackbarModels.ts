export interface SnackbarState {
	open: boolean;
	message: string;
	severity: "info" | "warning" | "success";
}
