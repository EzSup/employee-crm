import { Eye, EyeSlash } from "@phosphor-icons/react";
import React, { FC, useState } from "react";
import "./../../styles/components/auth/FormField.scss";
import { Button, FormLabel, Grid2, TextField, Typography } from "@mui/material";

interface FormFieldProps {
	name: string;
	label: string;
	required: boolean;
	type?: string;
	value: string;
	onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
	errors?: string[];
	placeholder?: string;
}

export const FormField: FC<FormFieldProps> = ({
	name,
	label,
	required = false,
	type = "text",
	value,
	onChange,
	errors = [],
	placeholder = "",
}) => {
	const [typeValue, setTypeValue] = useState<string>(type);
	const eyeIconSize = 30;
	function switchType() {
		setTypeValue(typeValue === "password" ? "text" : "password");
	}

	return (
		<Grid2 className="input-group">
			<FormLabel about={name}>
				{label}
				{required ? <span className="error-color-text">*</span> : <></>}
			</FormLabel>
			<Grid2 className="password-wrapper">
				<TextField
					sx={{ width: "100%" }}
					name={name}
					type={typeValue}
					value={value}
					onChange={onChange}
					placeholder={placeholder}
				/>
				{type === "password" && (
					<Button
						type="button"
						className="toggle-password"
						onClick={switchType}
					>
						{typeValue === "password" ? (
							<EyeSlash size={eyeIconSize} />
						) : (
							<Eye size={eyeIconSize} />
						)}
					</Button>
				)}
			</Grid2>
			{
				<Typography className="field-error-message">
					{errors?.length > 0 ? errors[0] : null}
				</Typography>
			}
		</Grid2>
	);
};
