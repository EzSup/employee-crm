import { Grid2, TextField, Typography, Stack } from "@mui/material";
import { Form } from "../../models/employeeDataModels";
import { DatePicker } from "@mui/x-date-pickers";
import { styled } from "@mui/material/styles";
import { FC } from "react";
import { Controller, Control, FieldErrors } from "react-hook-form";
import { Dayjs } from "dayjs";

interface MainDataEditingCardProps {
	control: Control<Form>;
	errors: FieldErrors<Form>;
}

const FieldsStack = styled(Grid2)(({ theme }) => ({
	display: "flex",
	flexDirection: "row",
	justifyContent: "space-between",
	gap: theme.spacing(1),
}));

const MainDataEditingCard: FC<MainDataEditingCardProps> = ({
	control,
	errors,
}: MainDataEditingCardProps) => {
	return (
		<Grid2 container spacing={1}>
			<Typography variant="h6">Main info</Typography>
			<Stack direction={"column"} spacing={1}>
				<FieldsStack>
					<Grid2>
						<Typography variant="caption">First Name (ENG)</Typography>
						<Controller
							name="fNameEn"
							control={control}
							render={({ field }) => (
								<TextField
									{...field}
									fullWidth
									size="small"
									error={!!errors?.fNameEn}
									helperText={errors?.fNameEn?.message}
								/>
							)}
						/>
					</Grid2>
					<Grid2>
						<Typography variant="caption">LastName (ENG)</Typography>
						<Controller
							name="lNameEn"
							control={control}
							render={({ field }) => (
								<TextField
									{...field}
									fullWidth
									size="small"
									error={!!errors?.lNameEn}
									helperText={errors?.lNameEn?.message}
								/>
							)}
						/>
					</Grid2>
					<Grid2>
						<Typography variant="caption">Middle Name (ENG)</Typography>
						<Controller
							name="mNameEn"
							control={control}
							render={({ field }) => (
								<TextField
									{...field}
									fullWidth
									size="small"
									error={!!errors?.mNameEn}
									helperText={errors?.mNameEn?.message}
								/>
							)}
						/>
					</Grid2>
				</FieldsStack>
				<FieldsStack>
					<Grid2>
						<Typography variant="caption">First Name (UKR)</Typography>
						<Controller
							name="fNameUk"
							control={control}
							render={({ field }) => (
								<TextField
									{...field}
									fullWidth
									size="small"
									error={!!errors?.fNameUk}
									helperText={errors?.fNameUk?.message}
								/>
							)}
						/>
					</Grid2>
					<Grid2>
						<Typography variant="caption">LastName (UKR)</Typography>
						<Controller
							name="lNameUk"
							control={control}
							render={({ field }) => (
								<TextField
									{...field}
									fullWidth
									size="small"
									error={!!errors?.lNameUk}
									helperText={errors?.lNameUk?.message}
								/>
							)}
						/>
					</Grid2>
					<Grid2>
						<Typography variant="caption">Middle Name (UKR)</Typography>
						<Controller
							name="mNameUk"
							control={control}
							render={({ field }) => (
								<TextField
									{...field}
									fullWidth
									size="small"
									error={!!errors?.mNameUk}
									helperText={errors?.mNameUk?.message}
								/>
							)}
						/>
					</Grid2>
				</FieldsStack>
				<FieldsStack>
					<Grid2>
						<Typography variant="caption">Personal Email</Typography>

						<Controller
							name="personalEmail"
							control={control}
							render={({ field }) => (
								<TextField
									{...field}
									fullWidth
									size="small"
									error={!!errors?.personalEmail}
									helperText={errors?.personalEmail?.message}
								/>
							)}
						/>
					</Grid2>
					<Grid2>
						<Typography variant="caption">Phone number</Typography>
						<Controller
							name="phoneNumber"
							control={control}
							render={({ field }) => (
								<TextField
									{...field}
									fullWidth
									size="small"
									error={!!errors?.phoneNumber}
									helperText={errors?.phoneNumber?.message}
								/>
							)}
						/>
					</Grid2>
					<Grid2>
						<Typography variant="caption">Previous work place</Typography>
						<Controller
							name="prevWorkPlace"
							control={control}
							render={({ field }) => (
								<TextField
									{...field}
									fullWidth
									size="small"
									error={!!errors?.prevWorkPlace}
									helperText={errors?.prevWorkPlace?.message}
								/>
							)}
						/>
					</Grid2>
				</FieldsStack>
				<FieldsStack sx={{ justifyContent: "flex-start" }}>
					<Grid2>
						<Typography variant="caption">Birth Date</Typography>
						<Grid2>
							<Controller
								name="birthDate"
								control={control}
								render={({ field }) => (
									<DatePicker
										{...field}
										format="DD/MM/YYYY"
										disableFuture
										slotProps={{
											textField: {
												error: !!errors?.birthDate,
												helperText: errors.birthDate?.message,
											},
										}}
									/>
								)}
							/>
						</Grid2>
					</Grid2>
					<Grid2>
						<Typography variant="caption">Application Date</Typography>
						<Grid2>
							<Controller
								name="applicationDate"
								control={control}
								render={({ field }) => (
									<DatePicker
										{...field}
										format="DD/MM/YYYY"
										disableFuture
										slotProps={{
											textField: {
												error: !!errors?.applicationDate,
												helperText: errors.applicationDate?.message,
											},
										}}
									/>
								)}
							/>
						</Grid2>
					</Grid2>
				</FieldsStack>
			</Stack>
		</Grid2>
	);
};

export default MainDataEditingCard;
