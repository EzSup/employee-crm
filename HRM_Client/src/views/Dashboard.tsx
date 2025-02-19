import { Grid2, Paper, Stack } from "@mui/material";
import React, { useEffect, useState } from "react";
import StatisticCard from "../components/dashboard/StatisticCard";
import "../styles/components/dashboard/Dashboard.scss";
import { File, Speaker, People } from "../components/icons";
import { getNotAcceptedForms } from "../services/employeesSevice";
import { Person } from "../models/employeeDataModels";
import { getTotalEmployeesCount } from "../services/statisticsService";
import AnnouncementsOfDashboard from "../components/dashboard/AnnouncementsOfDashboard";
import ReviewedFormsListOfDashboard from "../components/dashboard/ReviewedFormsListOfDashboard.tsx";

const Dashboard: React.FC = () => {
	const [forms, setForms] = useState<Person[]>([]);
	const [employeesCount, setEmployeesCount] = useState(0);
	useEffect(() => {
		const fetchData = async () => {
			setEmployeesCount(await getTotalEmployeesCount());
			setForms(await getNotAcceptedForms());
		};
		fetchData();
	}, []);

	return (
		<Grid2 className="dashboard-container ">
			<Grid2 margin={3} size={12} sx={{ flexShrink: 0 }}>
				<Stack direction="row" spacing={1}>
					<StatisticCard icon={<File />} message="Upcoming Events" count={14} />
					<StatisticCard icon={<Speaker />} message="Responses" count={88} />
					<StatisticCard
						icon={<People />}
						message="Total Employees"
						count={employeesCount}
					/>
				</Stack>
			</Grid2>

			<Grid2
				container
				spacing={2}
				size={12}
				margin={3}
				sx={{
					flex: 1,
					display: "flex",
				}}
			>
				<Grid2 size={8}>
					<Paper sx={{ height: "100%" }}>
						<AnnouncementsOfDashboard />
					</Paper>
				</Grid2>
				<Grid2 size={4}>
					<Paper sx={{ height: "100%" }}>
						<ReviewedFormsListOfDashboard forms={forms} />
					</Paper>
				</Grid2>
			</Grid2>
		</Grid2>
	);
};

export default Dashboard;
