import { Button, Grid2, Typography } from "@mui/material";
import PartnerEditingCard from "./PartnerEditingCard";
import { Form, Gender, Partner } from "../../models/employeeDataModels";
import { deletePartner } from "../../services/employeesSevice";
import { FC } from "react";
import { Control } from "react-hook-form";
import dayjs from "dayjs";

interface PartnerSectionProps {
	partner: Partner | null;
	control: Control<Form>;
	personId: number;
	setPartner: (value: Partner | null) => void;
}

const PartnerSection: FC<PartnerSectionProps> = ({
	partner,
	personId,
	setPartner,
}: PartnerSectionProps) => {
	return (
		<Grid2>
			<Typography variant="h6" sx={{ mb: 2 }}>
				Partner
			</Typography>
			{partner ? (
				<PartnerEditingCard
					item={partner}
					onChange={(item) => setPartner(item)}
					onDelete={() => {
						if (partner.id > 0) deletePartner(partner.id);
						setPartner(null);
					}}
				/>
			) : (
				<Button
					color="info"
					onClick={() => {
						setPartner({
							id: 0,
							personId: personId,
							name: "",
							birthDate: dayjs(new Date()),
							gender: Gender.Male,
						});
					}}
				>
					+
				</Button>
			)}
		</Grid2>
	);
};

export default PartnerSection;
