import { FC } from "react";
import { useSelector } from "react-redux";
import { RootState } from "../../store/store";
import "../../styles/components/auth/Usercard.scss";
import UserProfile from "../employeeslist/UserProfile";

const Usercard: FC = () => {
	const authContext = useSelector((state: RootState) => state.auth);

	return (
		<div>
			<UserProfile
				title={authContext?.user?.username ?? "unknown"}
				subtitle={authContext?.user?.fullName ?? "unknown"}
				photo={""}
			/>
		</div>
	);
};

export default Usercard;
