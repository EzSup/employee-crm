import { Person } from "../../models/employeeDataModels.ts";
import { Container, Stack, Typography } from "@mui/material";
import UserProfile from "../employeeslist/UserProfile.tsx";
import { format } from "date-fns";
import { styled } from "@mui/material/styles";
import { Link } from "react-router-dom";
import { FC, useEffect, useState } from "react";

interface ReviewedFormsListOfDashboardProps {
	forms: Person[];
}

const ReviewedFormsListOfDashboard: FC<ReviewedFormsListOfDashboardProps> = ({
	forms,
}: ReviewedFormsListOfDashboardProps) => {
	const LocalHeader = styled(Typography)(() => ({
		fontSize: 18,
		fontWeight: 400,
		margin: "15px 0",
	}));

	const [data, setData] = useState(forms);
	useEffect(() => {
		if (forms) {
			setData(forms);
		}
	}, [forms]);

	return (
		<Container>
			<Stack>
				<LocalHeader>Recieved Forms</LocalHeader>
				{data.map((item: Person) => (
					<Link to={`employeeform/${item.id}`} key={item.id}>
						<UserProfile
							photo={item.photo}
							title={`${item.fNameEn} ${item.lNameEn}`}
							subtitle={format(item.applicationDate, "d LLL H:mm")}
						/>
					</Link>
				))}
			</Stack>
		</Container>
	);
};

export default ReviewedFormsListOfDashboard;
