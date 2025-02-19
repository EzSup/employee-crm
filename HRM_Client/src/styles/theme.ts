import { createTheme } from "@mui/material/styles";

const theme = createTheme({
	colorSchemes: { light: true, dark: false },
	// breakpoints: {
	// 	values: {
	// 		xs: 0,
	// 		sm: 600,
	// 		md: 800,
	// 		lg: 1200,
	// 		xl: 1536,
	// 	},
	// },
	palette: {
		primary: {
			main: "#ef815d",
			contrastText: "#ffffff",
		},
		secondary: {
			main: "#8E95B2",
			contrastText: "#ffffff",
		},
		info: {
			main: "rgb(25, 118, 210)",
			contrastText: "#ffffff",
		},
		warning: {
			main: "#e84b18",
		},
	},
	components: {
		MuiButton: {
			styleOverrides: {
				text: {
					fontFamily: "'Inter', sans-serif",
					textTransform: "none",
					fontWeight: "400",
				},
			},
		},
		MuiTextField: {
			styleOverrides: {
				root: {
					fontFamily: "'Inter', sans-serif",
					backgroundColor: "#f6f6f6",
					border: "none",
					borderRadius: "8px",
				},
			},
		},
		MuiOutlinedInput: {
			styleOverrides: {
				notchedOutline: {
					border: "none",
					borderRadius: "8px",
				},
			},
		},
		MuiTypography: {
			styleOverrides: {
				caption: {
					fontFamily: "Inter",
					fontSize: "14px",
					fontWeight: 400,
					color: "#474747",
				},
				h3: {
					fontSize: 32,
					fontWeight: 400,
					margin: "15px 0",
				},
				h6: {
					fontFamily: "Inter",
					fontSize: "20px",
					marginBottom: "4px",
				},
				subtitle1: {
					fontSize: "16px",
					color: "#474747",
				},
			},
		},
	},
});

export default theme;
