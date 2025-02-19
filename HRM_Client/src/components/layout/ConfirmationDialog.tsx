import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  Typography,
} from "@mui/material";
import { FC, ReactNode, useState } from "react";

interface ConfirmationDialogProps {
  title: string;
  subtitle?: string;
  icon?: ReactNode;
  onConfirm: () => void;
  onRefuse?: () => void;
}

const ConfirmationDialog: FC<ConfirmationDialogProps> = ({
  title,
  subtitle,
  onConfirm,
  onRefuse,
}: ConfirmationDialogProps) => {
  const [open, setOpen] = useState<boolean>(true);

  const handleClickYes = () => {
    onConfirm();
    setOpen(false);
  };

  const handleClickNo = () => {
    if (onRefuse) onRefuse();
    setOpen(false);
  };

  return (
    <Dialog open={open}>
      <DialogContent dividers>
        <Typography gutterBottom>{title}</Typography>
        <Typography gutterBottom>{subtitle}</Typography>
      </DialogContent>
      <DialogActions>
        <Button onClick={handleClickYes}>Yes</Button>
        <Button variant="contained" onClick={handleClickNo}>
          No
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default ConfirmationDialog;
