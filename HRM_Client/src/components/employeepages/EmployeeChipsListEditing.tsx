import { AddCircle } from "@mui/icons-material";
import { Chip, Grid2, IconButton } from "@mui/material";
import { FC, useState, useEffect } from "react";
import "../../styles/components/employeepages/EmployeeChipsListEditing.scss";

interface EmployeeChipsListEditingProps {
	onChange: (value: string) => void;
	listData: string[];
	placeholder?: string;
}

const EmployeeListEditing: FC<EmployeeChipsListEditingProps> = ({
	listData,
	onChange,
	placeholder = "Enter value",
}) => {
	const [formVisible, setFormVisible] = useState<boolean>(false);
	const [newValue, setNewValue] = useState<string>("");

	const handlePressKeyInChip = (e: React.KeyboardEvent) => {
		if (e.key === "Enter") addNewValue();
	};

	const addNewValue = () => {
		if (newValue.trim().length > 0) {
			const updatedData = [...listData!, newValue.trim()];
			onChange(updatedData.join());
			setNewValue("");
		}
		setFormVisible(false);
	};

	return (
		<Grid2 container>
			{Array.isArray(listData) &&
				listData?.map((item: string, index: number) => (
					<Chip
						key={index}
						label={item}
						sx={{ m: "5px" }}
						onDelete={() =>
							onChange(listData.filter((x: string) => x !== item).join())
						}
					/>
				))}

			{formVisible ? (
				<div className="chip-input">
					<input
						value={newValue}
						onChange={(e) => setNewValue(e.target.value)}
						onKeyDown={handlePressKeyInChip}
						autoFocus
						placeholder={placeholder}
					/>
					<IconButton color="info" onClick={addNewValue} size="small">
						<AddCircle />
					</IconButton>
				</div>
			) : (
				<Chip
					onClick={() => setFormVisible(true)}
					sx={{ border: "dashed #1976d2 1px", color: "#1976d2", m: "5px" }}
					variant="outlined"
					label="Add more +"
				/>
			)}
		</Grid2>
	);
};

export default EmployeeListEditing;
