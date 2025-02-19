import { Button, Grid2, IconButton, Stack, Typography } from "@mui/material";
import { BagSimple, FileText, Copy } from "@phosphor-icons/react";
import { Link } from "react-router-dom";
import { Form, Gender } from "../../models/employeeDataModels";
import { Telegram } from "../icons";
import GenderSwitch from "./GenderSwitch";
import "../../styles/components/employeepages/EmployeeForm.scss";
import { getCV, getPassportScan } from "../../services/employeesSevice";
import { styled } from "@mui/material/styles";
import { FC, useState, useEffect } from "react";
import blankPhoto from "../../assets/userBlankPhoto.jpg";

interface EmployeeImageAndTelegramDataCardProps {
	form: Form | undefined;
	onGenderUpdate: (value: Gender) => void;
}
const EmployeeImageAndTelegramDataCard: FC<
	EmployeeImageAndTelegramDataCardProps
> = ({ form, onGenderUpdate }: EmployeeImageAndTelegramDataCardProps) => {
	const [formPicSrc, setFormPicSrc] = useState<string>(
		form?.photo ?? blankPhoto
	);

	useEffect(() => {
		if (form?.photo) {
			setFormPicSrc(form.photo);
		}
	}, [form?.photo]);

	const LocalButton = styled(IconButton)(() => ({
		background: "none",
		padding: "0",
		margin: "0",
	}));

	const LocalLink = styled(Link)(() => ({
		background: "none",
		padding: "0",
		margin: "0",
	}));

	const TgIdText = styled(Typography)(() => ({
		fontSize: 12,
		fontWeight: 400,
		color: "#A8A8A8",
	}));

	const getTelegramChatUrl = (): string => {
		return `https://t.me/${form?.telegramUserName}`;
	};

	const copyTelegramIdToClipboard = () => {
		navigator.clipboard.writeText(
			form?.telegramUserName ?? form?.telegramId.toString() ?? "nodata"
		);
	};

	const openCV = async () => {
		const link = await getCV(form?.id!);
		if (link) window.open(link);
	};

	const openDoc = async () => {
		const link = await getPassportScan(form?.id!);
		if (link) window.open(link);
	};
	return (
		<Grid2 container direction={"column"} sx={{ display: "grid" }}>
			<img
				className="person-img"
				src={formPicSrc}
				onError={() => setFormPicSrc(blankPhoto)}
			/>
			<Stack direction={"row"} className="person-photo-files-card" spacing={1}>
				<Button
					onClick={openCV}
					className="files-button"
					sx={{ backgroundColor: "#A78EB2" }}
				>
					CV
					<BagSimple size={20} />
				</Button>
				<Button
					onClick={openDoc}
					className="files-button"
					sx={{ backgroundColor: "#8eb2a5" }}
				>
					Doc
					<FileText size={20} />
				</Button>
				<GenderSwitch
					initialValue={form?.gender}
					onChange={(value: Gender) => onGenderUpdate(value)}
				/>
			</Stack>
			<Grid2
				container
				direction="row"
				alignItems="center"
				justifyContent={"center"}
				spacing={1}
			>
				<LocalLink
					to={getTelegramChatUrl()}
					target="_blank"
					rel="noopener noreferrer"
				>
					<Telegram />
				</LocalLink>
				<TgIdText>Telegram ID: {form?.telegramId}</TgIdText>
				<LocalButton onClick={copyTelegramIdToClipboard} disableRipple>
					<Copy size={20} />
				</LocalButton>
			</Grid2>
		</Grid2>
	);
};

export default EmployeeImageAndTelegramDataCard;
