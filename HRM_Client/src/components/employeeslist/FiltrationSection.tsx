import { FC } from "react";
import "../../styles/components/employeesList/FiltrationSection.scss";
import { CaretDown } from "@phosphor-icons/react";

interface FiltartionSectionProps {
	title: string;
	values: any[];
	enumType: { [key: number]: string };
	selectedValues: any[];
	onValueChange: (values: any[]) => void;
	isOpen: boolean;
	onToggle: (sectionId: string) => void;
	sectionId: string;
}

const FiltrationSection: FC<FiltartionSectionProps> = ({
	title,
	values,
	enumType,
	selectedValues,
	onValueChange,
	isOpen,
	onToggle,
	sectionId,
}: FiltartionSectionProps) => {
	const handleCheckboxChange = (value: any) => {
		if (selectedValues.includes(value)) {
			onValueChange(selectedValues.filter((v) => v !== value));
		} else {
			onValueChange([...selectedValues, value]);
		}
	};

	return (
		<div className="filtration-section">
			<span
				className="filtration-section-title"
				onClick={() => onToggle(sectionId)}
			>
				{title}{" "}
				<button className={`show-hide-button ${isOpen ? "button-active" : ""}`}>
					<CaretDown size={20} />
				</button>
			</span>
			<div className={`options-container ${isOpen ? "container-active" : ""}`}>
				{values.map((value) => (
					<label key={value} className="option">
						<input
							type="checkbox"
							checked={selectedValues.includes(value)}
							onChange={() => handleCheckboxChange(value)}
						/>
						{enumType[value]}
					</label>
				))}
			</div>
		</div>
	);
};

export default FiltrationSection;
