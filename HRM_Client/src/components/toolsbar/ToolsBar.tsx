import { FC } from "react";
import "../../styles/components/employeesList/ToolsBar.scss";
import { Funnel } from "@phosphor-icons/react";
import SearchBar from "./SearchBar";
import ActionsBar from "../toolsbar/ActionsBar";

interface ToolsBarProps {
  customTableVisible: boolean;
  onCustomTableVisibilityChange: (isVisible: boolean) => void;
  onNameFilterUpdate: (value: string) => void;
  searchBarPlaceholder: string;
  downloadAction?: () => void;
  deleteAction?: () => void;
  createLink?: string;
  editLink?: string;
}

const ToolsBar: FC<ToolsBarProps> = ({
  customTableVisible,
  onCustomTableVisibilityChange,
  onNameFilterUpdate,
  searchBarPlaceholder,
  downloadAction,
  deleteAction,
  createLink,
  editLink,
}: ToolsBarProps) => {
  return (
    <div className="tools-bar">
      <SearchBar
        onValueUpdate={onNameFilterUpdate}
        placeholder={searchBarPlaceholder}
      />
      <button
        className="filter-button"
        onClick={() => onCustomTableVisibilityChange(!customTableVisible)}
      >
        <Funnel size={20} />
      </button>

      <ActionsBar
        downloadAction={downloadAction}
        deleteAction={deleteAction}
        createLink={createLink}
        editLink={editLink}
      />
    </div>
  );
};

export default ToolsBar;
