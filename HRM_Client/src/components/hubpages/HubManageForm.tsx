import { FC, useEffect, useState } from "react";
import {
	HubCreateRequest,
	HubMember,
	HubUpdateRequest,
	toShortData,
} from "../../models/hubsDataModels";
import {
	deleteHub,
	getFreeEmployees,
	getHub as getHubById,
	putHub,
} from "../../services/hubsService";
import { Grid2, TextField, Typography } from "@mui/material";
import FormTitle from "../toolsbar/FormTitle";
import { Control, Controller, useForm, UseFormSetValue } from "react-hook-form";
import HubMembersAutocomplete from "./HubMembersAutocomplete";
import SelectLeader from "./SelectLeader";
import { useNavigate } from "react-router-dom";
import { hubUpdateDefaultValues } from "../../models/validationModels";

interface HubManageFormProps {
	id: number;
}

const HubManageForm: FC<HubManageFormProps> = ({ id }: HubManageFormProps) => {
	const {
		control,
		handleSubmit,
		reset,
		setValue,
		getValues,
		formState: { isValid, errors },
	} = useForm<HubUpdateRequest>(hubUpdateDefaultValues);

	const navigate = useNavigate();
	const [hubLeader, setHubLeader] = useState<string | null>();
	const [hubDeputyLeader, setHubDeputyLeader] = useState<string | null>();
	const [hubDirector, setHubDirector] = useState<string | null>();
	const [employees, setEmployees] = useState<HubMember[]>([]);
	const [selectedEmployees, setSelectedEmployees] = useState<string[]>([]);
	const [leaders, setLeaders] = useState<(string | null)[]>([
		hubLeader || null,
		hubDeputyLeader || null,
		hubDirector || null,
	]);

	useEffect(() => {
		const loadData = async () => {
			const hubData = await getHubById(id);
			if (hubData) {
				reset(hubData);
				setValue(
					"memberIds",
					hubData.employees.map((x) => x.id)
				);
			}
			const freeEmp = await getFreeEmployees();
			setEmployees(freeEmp.concat(hubData.employees));
		};
		loadData();
	}, []);

	useEffect(() => {
		setHubLeader(
			employees
				.filter((x) => x.id === getValues("leaderId"))
				.map((x) => toShortData(x))[0]
		);
		setHubDeputyLeader(
			employees
				.filter((x) => x.id === getValues("deputyLeaderId"))
				.map((x) => toShortData(x))[0]
		);
		setHubDirector(
			employees
				.filter((x) => x.id === getValues("directorId"))
				.map((x) => toShortData(x))[0]
		);
		setSelectedEmployees(
			employees
				.filter((x) => getValues("memberIds").includes(x.id))
				.map((x) => toShortData(x))
		);
		console.log(getValues());
	}, [getValues()]);

	useEffect(
		() =>
			setLeaders([
				hubLeader || null,
				hubDeputyLeader || null,
				hubDirector || null,
			]),
		[hubLeader, hubDeputyLeader, hubDirector]
	);

	return (
		<form
			onSubmit={handleSubmit(async (data) => {
				if (await putHub(data)) navigate("/hubs");
			})}
		>
			<Grid2 container spacing={2} padding={1} flexDirection={"column"}>
				<FormTitle
					primaryButtonTitle="Update"
					secondaryButtonTitle="Delete"
					title="Hub Manage Page"
					confirmationMessage={`Are you sure to remove ${getValues("name")}? All the members will be dismissed!`}
					onReject={async () => {
						if (await deleteHub(id)) navigate("/hubs");
					}}
				/>
				<Grid2 container spacing={3} wrap="wrap">
					<Grid2>
						<Typography variant="caption">Name</Typography>
						<Controller
							name="name"
							control={control}
							render={({ field }) => (
								<TextField
									{...field}
									placeholder="DevOps"
									fullWidth
									size="medium"
									error={!!errors?.name}
									helperText={errors?.name?.message}
								/>
							)}
						/>
					</Grid2>
					<SelectLeader
						title="Leader"
						control={control as unknown as Control<HubCreateRequest, any>}
						errors={errors?.leaderId}
						options={selectedEmployees.filter((x) => !leaders.includes(x))}
						value={hubLeader || null}
						setValue={(newValue) => {
							setHubLeader(newValue ?? null);
							setValue(
								"leaderId",
								employees.find((x) => toShortData(x) === newValue)?.id ||
									undefined
							);
						}}
						controllerName="leaderId"
					/>
					<SelectLeader
						title="Deputy Leader"
						control={control as unknown as Control<HubCreateRequest, any>}
						errors={errors?.deputyLeaderId}
						options={selectedEmployees.filter((x) => !leaders.includes(x))}
						value={hubDeputyLeader || null}
						setValue={(newValue) => {
							setHubDeputyLeader(newValue ?? null);
							setValue(
								"deputyLeaderId",
								employees.find((x) => toShortData(x) === newValue)?.id ||
									undefined
							);
						}}
						controllerName="deputyLeaderId"
					/>
					<SelectLeader
						title="Director"
						control={control as unknown as Control<HubCreateRequest, any>}
						errors={errors?.directorId}
						options={selectedEmployees.filter((x) => !leaders.includes(x))}
						value={hubDirector || null}
						controllerName="directorId"
						setValue={(newValue) => {
							setHubDirector(newValue ?? null);
							setValue(
								"directorId",
								employees.find((x) => toShortData(x) === newValue)?.id ||
									undefined
							);
						}}
					/>
					<Grid2 width={"100%"}>
						<Typography variant="caption">Members</Typography>
						<HubMembersAutocomplete
							control={control as unknown as Control<HubCreateRequest, any>}
							leaders={leaders}
							selectedEmployees={selectedEmployees}
							employees={employees}
							setSelectedEmployees={setSelectedEmployees}
							setValue={
								setValue as unknown as UseFormSetValue<HubCreateRequest>
							}
						/>
					</Grid2>
				</Grid2>
			</Grid2>
		</form>
	);
};

export default HubManageForm;
