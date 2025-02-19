import { Grid2, Typography } from "@mui/material";
import { FC } from "react";

const PageNotFound: FC = () => {
	return (
		<Grid2
			container
			justifyContent="center"
			alignContent={"center"}
			height={"100%"}
		>
			<Typography variant="h1">404 PageNotFound</Typography>
		</Grid2>
	);
};

export default PageNotFound;
