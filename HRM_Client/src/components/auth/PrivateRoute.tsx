import { useDispatch, useSelector } from "react-redux";
import { Navigate, Outlet, useNavigate } from "react-router-dom";
import { RootState } from "../../store/store";
import { logout } from "../../services/authService";
import { FC } from "react";

const PrivateRoute: FC = () => {
	const authContext = useSelector((state: RootState) => state.auth);
	if (authContext.refreshTerm && authContext.refreshTerm < new Date()) {
		const dispatch = useDispatch();
		const navigate = useNavigate();
		logout(dispatch, navigate);
	}
	if (authContext?.user.isAuthenticated === false)
		return <Navigate to="/login" />;

	return <Outlet />;
};

export default PrivateRoute;
