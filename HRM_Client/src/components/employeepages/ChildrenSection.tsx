import { Button, Grid2, Stack, Typography } from "@mui/material";
import ChildEditingCard from "./ChildEditingCard";
import { Child, Gender } from "../../models/employeeDataModels.ts";
import { deleteChild } from "../../services/employeesSevice";
import { FC } from "react";
import dayjs from "dayjs";

interface ChildrenSectionProps {
	childrenArray: Child[] | undefined;
	setChildrenArray: (value: Child[] | undefined) => void;
}

const ChildrenSection: FC<ChildrenSectionProps> = ({
	childrenArray,
	setChildrenArray,
}: ChildrenSectionProps) => {
	const handleChildUpdate = (child: Child, targetChildIndex: number) => {
		setChildrenArray(
			childrenArray?.map((item, index) =>
				index === targetChildIndex ? child : item
			)
		);
	};

	const handleChildDelete = (childId: number, indexInArray: number) => {
		if (childId > 0) deleteChild(childId);
		setChildrenArray(
			childrenArray?.filter((_, index) => index != indexInArray) ?? []
		);
	};

	const addEmptyChild = () => {
		setChildrenArray([
			...(childrenArray ?? []),
			{
				id: 0,
				name: "",
				birthDate: dayjs(new Date()),
				gender: Gender.Male,
			},
		]);
	};

	return (
		<Grid2>
			<Typography variant="h6" sx={{ mb: 2 }}>
				Children
			</Typography>
			<Stack direction="row" flexWrap={"wrap"}>
				{childrenArray?.map((item, indexHigher) => (
					<ChildEditingCard
						key={item.id}
						item={item}
						onChange={(child) => handleChildUpdate(child, indexHigher)}
						onDelete={() => handleChildDelete(item.id, indexHigher)}
					/>
				))}
				<Button color="info" onClick={addEmptyChild}>
					+
				</Button>
			</Stack>
		</Grid2>
	);
};

export default ChildrenSection;
