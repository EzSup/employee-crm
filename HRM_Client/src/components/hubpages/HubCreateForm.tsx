import { Grid2, TextField, Typography } from "@mui/material";
import { FC, useEffect, useState } from "react";
import FormTitle from "../toolsbar/FormTitle";
import { Controller, useForm } from "react-hook-form";
import {
	HubMember,
	HubCreateRequest,
	toShortData,
} from "../../models/hubsDataModels";
import { getFreeEmployees, postHub } from "../../services/hubsService";
import { useNavigate } from "react-router-dom";
import SelectLeader from "./SelectLeader";
import HubMembersAutocomplete from "./HubMembersAutocomplete";
import { hubCreateDefaultValues } from "../../models/validationModels";

const HubCreateForm: FC = () => {
	const {
		control,
		handleSubmit,
		reset,
		setValue,
		getValues,
		formState: { isValid, errors },
	} = useForm<HubCreateRequest>(hubCreateDefaultValues);

	const navigate = useNavigate();
	const [hubLeader, setHubLeader] = useState<string | null>();
	const [hubDeputyLeader, setHubDeputyLeader] = useState<string | null>();
	const [hubDirector, setHubDirector] = useState<string | null>();
	const [employees, setEmployees] = useState<HubMember[]>([]);

	useEffect(() => {
		const loadEmployees = async () => {
			setEmployees(await getFreeEmployees());
		};
		loadEmployees();
	}, []);

	const [leaders, setLeaders] = useState<(string | null)[]>([
		hubLeader || null,
		hubDeputyLeader || null,
		hubDirector || null,
	]);
	useEffect(
		() =>
			setLeaders([
				hubLeader || null,
				hubDeputyLeader || null,
				hubDirector || null,
			]),
		[hubLeader, hubDeputyLeader, hubDirector]
	);

	const [selectedEmployees, setSelectedEmployees] = useState<string[]>(
		employees
			.filter((x) => leaders.includes(toShortData(x)))
			.map((x) => toShortData(x))
	);

	return (
		<form
			onSubmit={handleSubmit(async (data) => {
				if (await postHub(data)) navigate("/hubs");
			})}
		>
			<Grid2 container spacing={2} padding={1} flexDirection={"column"}>
				<FormTitle primaryButtonTitle="Confirm" title="Hub Create Form" />
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
						control={control}
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
						control={control}
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
						control={control}
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
							control={control}
							employees={employees}
							leaders={leaders}
							selectedEmployees={selectedEmployees}
							setSelectedEmployees={setSelectedEmployees}
							setValue={setValue}
						/>
					</Grid2>
				</Grid2>
			</Grid2>
		</form>
	);
};

export default HubCreateForm;
