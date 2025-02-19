import { FC } from "react";
import { CustomCellRendererProps } from "@ag-grid-community/react";
import "../../../styles/components/employeesList/UserCellRenderer.scss";
import UserProfile from "../UserProfile";

const CustomUserRenderer: FC<CustomCellRendererProps> = ({ value }) => {
	return (
		<UserProfile
			photo={value.photo}
			title={`${value.fNameEn} ${value.lNameEn}`}
			subtitle={`${value.fNameUk} ${value.lNameUk}`}
		/>
	);
};

export default CustomUserRenderer;
