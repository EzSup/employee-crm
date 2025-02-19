import { KeyboardArrowDown } from "@mui/icons-material";
import { Button, Grid2, Typography } from "@mui/material";
import "../../styles/components/dashboard/Dashboard.scss";
import { styled } from "@mui/material/styles";
import { NavLink } from "react-router-dom";
import { FC } from "react";

const AnnouncementsToolsList: FC = () => {
	const LocalContainer = styled(Grid2)(() => ({
		padding: "0px 20px",
		display: "flex",
		alignItems: "center",
		justifyContent: "space-between",
	}));

	const LocalButtonsContainer = styled(Grid2)(({ theme }) => ({
		display: "flex",
		alignItems: "center",
		gap: theme.spacing(2),
	}));

	const LocalHeader = styled(Typography)(({ theme }) => ({
		fontSize: theme.typography.h6.fontSize,
		fontWeight: theme.typography.fontWeightRegular,
		margin: theme.spacing(2, 0),
	}));

	const ViewAllLinkHeader = styled(Typography)(({ theme }) => ({
		fontSize: theme.typography.body2.fontSize,
		fontWeight: theme.typography.fontWeightRegular,
		margin: theme.spacing(2, 0),
	}));

	const ViewAllLink = styled(NavLink)(({ theme }) => ({
		textDecoration: "none",
		color: theme.palette.info.main,
	}));

	return (
		<LocalContainer>
			<Grid2 size="auto">
				<LocalHeader>Announcements</LocalHeader>
			</Grid2>
			<LocalButtonsContainer>
				<Button color="info" className="create-announcement-btn">
					Create announcement <KeyboardArrowDown />
				</Button>
				<ViewAllLinkHeader>
					<ViewAllLink to="">View all</ViewAllLink>
				</ViewAllLinkHeader>
			</LocalButtonsContainer>
		</LocalContainer>
	);
};

export default AnnouncementsToolsList;
