import { Button, Grid2, Stack, styled, Typography } from "@mui/material";
import ConfirmationDialog from "../layout/ConfirmationDialog";
import { FC, useState } from "react";
import "../../styles/globals/App.scss";

interface FormTitleProps {
  title: string;
  subtitle?: string;
  primaryButtonTitle: string;
  secondaryButtonTitle?: string;
  onReject?: () => void;
  confirmationMessage?: string;
}

const FormTitle: FC<FormTitleProps> = ({
  title,
  subtitle,
  confirmationMessage,
  primaryButtonTitle,
  secondaryButtonTitle,
  onReject,
}: FormTitleProps) => {
  const LocalSubtitle = styled(Typography)(() => ({
    fontSize: 18,
    fontWeight: 400,
    margin: "15px 0",
    color: "#A8A8A8",
    marginLeft: "20px",
    marginTop: "20px",
  }));

  const [rejectDialogOpen, setRejectDialogOpen] = useState<boolean>(false);
  return (
    <Stack direction={"row"} justifyContent={"space-between"} display={"flex"}>
      {rejectDialogOpen && (
        <ConfirmationDialog
          title={confirmationMessage ?? "Confirm action?"}
          onConfirm={() => (onReject ? onReject() : () => {})}
          onRefuse={() => setRejectDialogOpen(false)}
        />
      )}
      <Stack direction={"row"} alignItems={"center"}>
        <Typography variant="h3">{title}</Typography>
        {subtitle && <LocalSubtitle>{subtitle}</LocalSubtitle>}
      </Stack>
      <Grid2 className="control-buttons-section">
        <Button color="primary" variant="contained" type="submit">
          {primaryButtonTitle}
        </Button>
        {secondaryButtonTitle && (
          <Button
            color="secondary"
            variant="contained"
            onClick={() => setRejectDialogOpen(true)}
          >
            {secondaryButtonTitle}
          </Button>
        )}
      </Grid2>
    </Stack>
  );
};

export default FormTitle;
