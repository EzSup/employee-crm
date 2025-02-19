import { Grid2, Stack, Typography } from "@mui/material";
import { FC, useEffect } from "react";
import { Form } from "../../models/employeeDataModels.ts";
import {
	getFormById,
	refuseForm,
	updateAndApproveForm,
} from "../../services/employeesSevice.ts";
import { useNavigate, useParams } from "react-router-dom";
import "../../styles/components/employeepages/EmployeeForm.scss";
import EmployeeImageAndTelegramDataCard from "./EmployeeImageAndTelegramDataCard.tsx";
import MainDataEditingCard from "./MainDataEditingCard.tsx";
import EmployeeFormLanguageSelector from "./EmployeeFormLanguageSelector.tsx";
import EmployeeListEditing from "./EmployeeChipsListEditing.tsx";
import PartnerSection from "./PartnerSection.tsx";
import ChildrenSection from "./ChildrenSection.tsx";
import { useForm, Controller } from "react-hook-form";
import dayjs from "dayjs";
import FormTitle from "../toolsbar/FormTitle.tsx";
import { dateToString } from "../../services/helperService.ts";
import { formDefaultValues } from "../../models/validationModels.ts";

const EmployeeForm: FC = () => {
	const {
		control,
		handleSubmit,
		reset,
		setValue,
		getValues,
		formState: { isValid, errors },
	} = useForm<Form>(formDefaultValues);

	console.log(`form is ${isValid}`);

	const { id } = useParams();
	const idNumber = id ? parseInt(id) : 1;

	useEffect(() => {
		const fetchData = async () => {
			try {
				const data = await getFormById(idNumber);
				reset({
					...data,
					partner: data.partner
						? {
								...data.partner,
								personId: data.id,
							}
						: undefined,
					birthDate: dayjs(data.birthDate),
					applicationDate: dayjs(data.applicationDate),
					hobbies: data.hobbies,
				});
			} catch (error) {
				console.log(error);
			}
		};
		fetchData();
	}, [idNumber, reset]);

	const navigate = useNavigate();

	const refuse = async () => {
		await refuseForm(idNumber);
		navigate("/");
	};

	return (
		<form
			onSubmit={handleSubmit(async (data) => {
				await updateAndApproveForm(data);
				navigate("/");
			})}
		>
			<Grid2 container spacing={2} padding={1} flexDirection={"column"}>
				{
					<FormTitle
						title="Application Form"
						subtitle={dateToString(value.applicationDate)}
						primaryButtonTitle="Approve"
						secondaryButtonTitle="Reject"
						onReject={refuse}
						confirmationMessage={`Are you sure to refuse: ${value.fNameEn} ${value.lNameEn} `}
					/>
				}
				<Grid2 container spacing={3} wrap="wrap">
					<Grid2 sx={{ maxWidth: 360 }} size={{ xs: 12, sm: 6, md: 4 }}>
						{getValues() && (
							<EmployeeImageAndTelegramDataCard
								form={getValues()}
								onGenderUpdate={(value) => setValue("gender", value)}
							/>
						)}
					</Grid2>
					<Grid2 size={{ xs: 12, sm: 6, md: 8 }} sx={{ paddingRight: "10px" }}>
						{<MainDataEditingCard control={control} errors={errors} />}
					</Grid2>
				</Grid2>
				<Grid2>
					{
						<Typography variant="h6" sx={{ mb: 2 }}>
							Skills and hobbies
						</Typography>
					}
					{
						<Stack direction={"row"} gap={4}>
							<Grid2 container direction={"column"} size="auto">
								<Typography variant="subtitle1">Languages</Typography>
								<EmployeeFormLanguageSelector
									title="English"
									currentValue={getValues().englishLevel}
									onchange={(value) => {
										setValue("englishLevel", value);
									}}
								/>
							</Grid2>
							{
								<Grid2 container direction={"row"}>
									<Grid2 container direction={"column"} size={12}>
										<Typography variant="subtitle1">Hobbies</Typography>
										<Controller
											name="hobbies"
											control={control}
											render={({ field }) => (
												<EmployeeListEditing
													listData={field.value ? field.value.split(",") : []}
													onChange={(value: string) => {
														setValue("hobbies", value);
													}}
												/>
											)}
										/>
									</Grid2>
									<Grid2 container direction={"column"} size={12}>
										<Typography variant="subtitle1">Tech stack</Typography>
										<Controller
											name="techStack"
											control={control}
											render={({ field }) => (
												<EmployeeListEditing
													listData={field.value ? field.value.split(",") : []}
													onChange={(value: string) => {
														setValue("techStack", value);
													}}
												/>
											)}
										/>
									</Grid2>
								</Grid2>
							}
						</Stack>
					}
				</Grid2>
				{
					<Grid2 container>
						<Controller
							name="partner"
							control={control}
							render={({ field }) => (
								<PartnerSection
									partner={field.value}
									control={control}
									personId={getValues().id ?? 1}
									setPartner={(value) => setValue("partner", value)}
								/>
							)}
						/>
						{
							<Controller
								name="children"
								control={control}
								render={({ field }) => (
									<ChildrenSection
										childrenArray={field.value}
										setChildrenArray={(value) => setValue("children", value)}
									/>
								)}
							/>
						}
					</Grid2>
				}
			</Grid2>
		</form>
	);
};

export default EmployeeForm;
