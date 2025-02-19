import { Card, CardContent, Container, Typography } from "@mui/material";
import "../../styles/components/dashboard/StatisticsCard.scss";
import { FC, ReactNode } from "react";

interface StatisticCardProps {
	icon: ReactNode;
	message: string;
	count: number;
}

const StatisticCard: FC<StatisticCardProps> = ({ icon, message, count }) => {
	return (
		<Container>
			<Card className="card">
				<CardContent>
					<div className="icon-container">{icon}</div>
					<Typography gutterBottom component="span">
						{message}
					</Typography>
					<Typography component="h5">{count}</Typography>
				</CardContent>
			</Card>
		</Container>
	);
};

export default StatisticCard;
