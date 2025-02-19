import { FC } from "react";
import { ChildAndPartnerBase, Partner } from "../../models/employeeDataModels";
import ChildOrPartnerEditingCard from "./ChildOrPartnerEditingCard";

interface PartnerEditingCardProps {
	item: Partner;
	onChange: (child: Partner) => void;
	onDelete: () => void;
}

const PartnerEditingCard: FC<PartnerEditingCardProps> = ({
	item,
	onChange,
	onDelete,
}: PartnerEditingCardProps) => {
	return (
		<ChildOrPartnerEditingCard
			item={item}
			onChange={(child: ChildAndPartnerBase) => {
				onChange(child as unknown as Partner);
			}}
			onDelete={onDelete}
		/>
	);
};

export default PartnerEditingCard;
