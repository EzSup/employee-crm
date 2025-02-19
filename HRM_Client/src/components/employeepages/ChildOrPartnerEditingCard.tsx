import {
	Card,
	CardActions,
	CardContent,
	Divider,
	TextField,
	Typography,
	Stack,
	IconButton,
	Grid2,
} from "@mui/material";
import { DatePicker } from "@mui/x-date-pickers";
import dayjs from "dayjs";
import GenderSwitch from "./GenderSwitch";
import { ChildAndPartnerBase } from "../../models/employeeDataModels";
import { Delete } from "@mui/icons-material";
import { FC, useState } from "react";
import ConfirmationDialog from "../layout/ConfirmationDialog";
import { styled } from "@mui/material/styles";

interface ChildOrPartnerEditingCardProps {
	item: ChildAndPartnerBase;
	onChange: (child: ChildAndPartnerBase) => void;
	onDelete: () => void;
}

const LocalCard = styled(Card)(() => ({
	maxHeight: "max-content",
	width: "max-content",
	mx: "auto",
	overflow: "auto",
	m: "5px",
}));

const ChildOrPartnerEditingCard: FC<ChildOrPartnerEditingCardProps> = ({
	item,
	onChange,
	onDelete,
}: ChildOrPartnerEditingCardProps) => {
	const [dialogOpen, setDialogOpen] = useState<boolean>(false);

	return (
		<LocalCard variant="outlined">
			{dialogOpen && (
				<ConfirmationDialog
					title={`Are you sure to remove: ${item.name}?`}
					onConfirm={onDelete}
					onRefuse={() => setDialogOpen(false)}
				/>
			)}
			<CardActions>
				<IconButton onClick={() => setDialogOpen(true)}>
					<Delete />
				</IconButton>
			</CardActions>
			<Divider />
			<CardContent>
				<Typography variant="caption">Name</Typography>
				<TextField
					fullWidth
					variant="outlined"
					size="small"
					name="lNameEn"
					value={item.name}
					onChange={(e) => onChange({ ...item, name: e.target.value })}
				/>
				<Stack sx={{ marginTop: "14px" }} direction={"row"}>
					<Grid2>
						<Typography variant="caption">Birth Date</Typography>
						<Grid2>
							<DatePicker
								value={dayjs(item.birthDate)}
								disableFuture
								name="birthDate"
								onChange={(day) => {
									onChange({ ...item, birthDate: day! });
								}}
							/>
						</Grid2>
					</Grid2>

					<Grid2 sx={{ margin: "auto 8px" }}>
						<GenderSwitch
							initialValue={item.gender}
							onChange={(gender) => onChange({ ...item, gender })}
						/>
					</Grid2>
				</Stack>
			</CardContent>
		</LocalCard>
	);
};

export default ChildOrPartnerEditingCard;
