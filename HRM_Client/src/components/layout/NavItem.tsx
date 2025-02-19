import { FC, ReactNode } from "react";
import "../../styles/globals/layout/NavItem.scss";
import { Link, useLocation } from "react-router-dom";

interface NavItemProps {
	destLink: string;
	label: string;
	icon?: ReactNode;
}

const NavItem: FC<NavItemProps> = ({ destLink, label, icon }: NavItemProps) => {
	const isActive = useLocation().pathname === destLink;

	return (
		<Link to={destLink} className={`nav-item ${isActive ? "active" : ""}`}>
			{icon}
			{label}
		</Link>
	);
};

export default NavItem;
