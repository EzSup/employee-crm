import { FC } from "react";
import { NavLink } from "react-router-dom";
import Logo from "./Logo";

const Header: FC = () => {
	return (
		<NavLink to="/">
			<Logo />
		</NavLink>
	);
};

export default Header;
