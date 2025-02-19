import {
	Stack,
	Table,
	TableBody,
	TableCell,
	TableContainer,
	TableHead,
	TableRow,
} from "@mui/material";
import AnnouncementsToolsList from "./AnnouncementsToolsList";
import { FC } from "react";

const AnnouncementsOfDashboard: FC = () => {
	return (
		<Stack>
			<AnnouncementsToolsList />
			<TableContainer>
				<Table sx={{ width: "100%" }} size="small">
					<TableHead>
						<TableRow>
							<TableCell>Title</TableCell>
							<TableCell align="right">Posts</TableCell>
							<TableCell align="right">Last posts</TableCell>
							<TableCell align="right">Status</TableCell>
						</TableRow>
					</TableHead>
					<TableBody></TableBody>
				</Table>
			</TableContainer>
		</Stack>
	);
};

export default AnnouncementsOfDashboard;
