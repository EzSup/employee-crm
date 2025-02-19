import {
  DownloadSimple,
  ListBullets,
  PencilSimpleLine,
  Plus,
  TrashSimple,
} from "@phosphor-icons/react";
import { NavLink } from "react-router-dom";

interface ActionsBarProps {
  createLink?: string | undefined;
  editLink?: string | undefined;
  deleteAction?: () => void;
  downloadAction?: () => void;
  listLink?: string | undefined;
}

const ActionsBar = ({
  createLink,
  editLink,
  deleteAction,
  downloadAction,
  listLink,
}: ActionsBarProps) => {
  return (
    <div className="actions-bar">
      {createLink && (
        <NavLink to={createLink}>
          <Plus size={20} weight="regular" />
        </NavLink>
      )}
      {editLink && (
        <NavLink to={editLink}>
          <PencilSimpleLine size={20} weight="regular" />
        </NavLink>
      )}
      {deleteAction && (
        <NavLink to="" onClick={deleteAction}>
          <TrashSimple size={20} weight="regular" />
        </NavLink>
      )}
      {downloadAction && (
        <NavLink to={""} onClick={downloadAction}>
          <DownloadSimple size={20} weight="regular" />
        </NavLink>
      )}
      {listLink && (
        <NavLink to={listLink}>
          <ListBullets size={20} weight="regular" />
        </NavLink>
      )}
      <NavLink to="">
        <ListBullets size={20} weight="regular" />
      </NavLink>
    </div>
  );
};

export default ActionsBar;
