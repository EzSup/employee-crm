import { Grid2, NativeSelect, Typography } from "@mui/material";
import { styled } from "@mui/material/styles";
import { EnglishLevel, LANGUAGE_LEVELS } from "../../models/employeeDataModels";
import { FC } from "react";

interface EmployeeFormLanguageSelectorProps {
	title: string;
	currentValue: EnglishLevel | undefined;
	onchange: (value: EnglishLevel) => void;
}

const EmployeeFormLanguageSelector: FC<EmployeeFormLanguageSelectorProps> = ({
	title,
	currentValue,
	onchange,
}: EmployeeFormLanguageSelectorProps) => {
	const LocalSelect = styled(NativeSelect)(() => ({
		borderRadius: "10px",
		minWidth: "70px",
		marginLeft: "10px",
	}));

	return (
		<Grid2>
			<Typography variant="span">{title}</Typography>
			<LocalSelect
				id="demo-simple-select"
				size="small"
				onChange={(event) =>
					onchange(event.target.value as unknown as EnglishLevel)
				}
				defaultValue={currentValue}
			>
				{LANGUAGE_LEVELS.map((item) => (
					<option key={item.value} value={item.value}>
						{item.label}
					</option>
				))}
			</LocalSelect>
		</Grid2>
	);
};

export default EmployeeFormLanguageSelector;
