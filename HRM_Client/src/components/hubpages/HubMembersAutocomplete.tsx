import { FC, useMemo } from "react";
import Autocomplete, {
  AutocompleteRenderGetTagProps,
} from "@mui/material/Autocomplete";
import Chip from "@mui/material/Chip";
import TextField from "@mui/material/TextField";
import { Control, Controller, UseFormSetValue } from "react-hook-form";
import { Grid2 } from "@mui/material";
import {
  HubCreateRequest,
  HubMember,
  toShortData,
} from "../../models/hubsDataModels";

interface HubMembersAutocompleteProps {
  employees: HubMember[];
  leaders: (string | null)[];
  control: Control<HubCreateRequest, any>;
  selectedEmployees: string[];
  setSelectedEmployees: (employees: string[]) => void;
  setValue: UseFormSetValue<HubCreateRequest>;
}

const HubMembersAutocomplete: FC<HubMembersAutocompleteProps> = ({
  employees,
  leaders,
  control,
  selectedEmployees,
  setSelectedEmployees,
  setValue,
}) => {
  const handleEmployeeChange = (newValue: string[]) => {
    const updatedEmployees = [
      ...leaders.filter((x) => x !== undefined && x !== null),
      ...newValue.filter((option) => !leaders.includes(option)),
    ];
    setSelectedEmployees(updatedEmployees);

    const memberIds = employees
      .filter(
        (x) =>
          updatedEmployees.some((item) => toShortData(x) === item) ||
          leaders.some((item) => toShortData(x) === item)
      )
      .map((x) => x.id);

    setValue("memberIds", memberIds);
  };

  const renderTag = (
    option: string,
    index: number,
    getTagProps: AutocompleteRenderGetTagProps
  ) => {
    const { key, ...tagProps } = getTagProps({ index });
    const disabled = leaders.includes(option);

    return (
      <Grid2
        key={key}
        title={disabled ? "Unremovable because is assigned as a leader!" : ""}
      >
        <Chip
          variant="outlined"
          label={option}
          {...tagProps}
          disabled={disabled}
        />
      </Grid2>
    );
  };

  const options = useMemo(
    () => employees.map((option) => toShortData(option)),
    [employees]
  );

  return (
    <Autocomplete
      multiple
      value={selectedEmployees}
      onChange={(event, newValue) => handleEmployeeChange(newValue as string[])}
      options={options}
      renderTags={(value, getTagProps) =>
        value.map((option, index) => renderTag(option, index, getTagProps))
      }
      renderInput={(params) => (
        <Controller
          name="memberIds"
          control={control}
          render={({ field }) => (
            <TextField
              {...field}
              sx={{ minWidth: "400px" }}
              {...params}
              variant="outlined"
            />
          )}
        />
      )}
    />
  );
};

export default HubMembersAutocomplete;
