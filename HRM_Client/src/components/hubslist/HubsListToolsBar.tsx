import { FC } from "react";
import SearchBar from "../toolsbar/SearchBar.tsx";
import "../../styles/components/employeesList/SearchBar.scss";
import { Funnel } from "@phosphor-icons/react";
import ActionsBar from "../toolsbar/ActionsBar.tsx";

interface HubsListToolsBarProps {
  customTableVisible: boolean;
  onCustomTableVisibilityChange: (isVisible: boolean) => void;
  onNameFilterUpdate: (value: string) => void;
}

const HubsListToolsBar: FC<HubsListToolsBarProps> = ({
  customTableVisible,
  onCustomTableVisibilityChange,
  onNameFilterUpdate,
}: HubsListToolsBarProps) => {
  return (
    <div className="tools-bar">
      <SearchBar onValueUpdate={onNameFilterUpdate} placeholder="Search hub" />
      <button
        className="filter-button"
        onClick={() => onCustomTableVisibilityChange(!customTableVisible)}
      >
        <Funnel size={20} />
      </button>

      <ActionsBar />
    </div>
  );
};

export default HubsListToolsBar;
