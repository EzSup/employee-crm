import { Grid2, Typography, Autocomplete, TextField } from "@mui/material";
import React, { FC } from "react";
import { Control, Controller, FieldError, FieldErrors } from "react-hook-form";
import { HubCreateRequest } from "../../models/hubsDataModels";

interface SelectMemberProps {
  title: string;
  value: string | null;
  setValue: (value: string | null) => void;
  options: string[];
  control: Control<HubCreateRequest, any>;
  errors: FieldError | undefined;
  controllerName: string;
}

const SelectLeader: FC<SelectMemberProps> = ({
  title,
  value,
  setValue,
  options,
  control,
  errors,
  controllerName,
}: SelectMemberProps) => {
  return (
    <Grid2>
      <Typography variant="caption">{title}</Typography>
      <Autocomplete
        disablePortal
        value={value || null}
        onChange={(event, newValue) => setValue(newValue ?? null)}
        options={options}
        sx={{ maxWidth: 500, minWidth: 300 }}
        renderInput={(params) => (
          <Controller
            name={controllerName as any}
            control={control}
            render={({ field }) => (
              <TextField
                {...params}
                {...field}
                error={!!errors}
                helperText={errors?.message}
              />
            )}
          />
        )}
      />
    </Grid2>
  );
};

export default SelectLeader;
