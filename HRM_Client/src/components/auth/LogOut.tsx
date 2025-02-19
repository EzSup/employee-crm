import { FC } from "react";
import { logout } from "../../services/authService";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";

const LogOut: FC = () => {
	logout(useDispatch(), useNavigate());
	return <div></div>;
};

export default LogOut;
