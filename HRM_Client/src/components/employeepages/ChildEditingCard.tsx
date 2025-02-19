import { FC } from "react";
import { Child, ChildAndPartnerBase } from "../../models/employeeDataModels";
import ChildOrPartnerEditingCard from "./ChildOrPartnerEditingCard";

interface ChildEditingCardProps {
	item: Child;
	onChange: (child: Child) => void;
	onDelete: () => void;
}

const ChildEditingCard: FC<ChildEditingCardProps> = ({
	item,
	onChange,
	onDelete,
}: ChildEditingCardProps) => {
	return (
		<ChildOrPartnerEditingCard
			item={item}
			onChange={(child: ChildAndPartnerBase) =>
				onChange(child as unknown as Child)
			}
			onDelete={onDelete}
		/>
	);
};

export default ChildEditingCard;
