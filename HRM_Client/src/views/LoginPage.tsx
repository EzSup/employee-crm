import "./../styles/view/Login.scss";
import { Login } from "../components/auth/Login";
import { Grid2 } from "@mui/material";

const LoginPage: React.FC = () => {
	return (
		<Grid2 className="container">
			<Login />
		</Grid2>
	);
};

export default LoginPage;
