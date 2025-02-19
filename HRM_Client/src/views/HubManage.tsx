import { FC } from "react";
import { useParams } from "react-router-dom";
import HubManageForm from "../components/hubpages/HubManageForm";

const HubManage: FC = () => {
  const { id } = useParams();
  return <HubManageForm id={id as unknown as number} />;
};

export default HubManage;
