import "../../styles/components/employeepages/GenderSwitch.scss";
import { Gender } from "../../models/employeeDataModels";
import { FC } from "react";

interface GenderSwitchProps {
	initialValue: Gender | undefined;
	onChange: (value: Gender) => void;
}

const GenderSwitch: FC<GenderSwitchProps> = ({
	initialValue,
	onChange,
}: GenderSwitchProps) => {
	const handleClick = (newValue: Gender) => {
		onChange?.(newValue);
	};

	return (
		<div className="gender-switch">
			<button
				className={`left ${initialValue === Gender.Male ? "selected" : ""}`}
				onClick={() => handleClick(Gender.Male)}
			>
				M
			</button>
			<button
				className={`right ${initialValue === Gender.Female ? "selected" : ""}`}
				onClick={() => handleClick(Gender.Female)}
			>
				F
			</button>
		</div>
	);
};

export default GenderSwitch;
