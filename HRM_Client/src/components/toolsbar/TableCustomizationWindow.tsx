import { FC } from "react";
import { EntitiesFields } from "../../models/employeeDataModels";

interface TableCustomizationProps {
  allAvailableFields: EntitiesFields[];
  selectedFields: EntitiesFields[];
  necessaryFields?: string[];
  onSelectedFieldsChange: (fields: EntitiesFields[]) => void;
}

const TableCustomizationWindow: FC<TableCustomizationProps> = ({
  allAvailableFields,
  selectedFields,
  onSelectedFieldsChange,
  necessaryFields,
}: TableCustomizationProps) => {
  const handleFieldToggle = (field: EntitiesFields) => {
    const newFields = selectedFields.includes(field)
      ? selectedFields.filter((f) => f !== field)
      : [...selectedFields, field];
    onSelectedFieldsChange(newFields);
  };

  const clearFilters = () => {
    onSelectedFieldsChange([]);
  };

  return (
    <div className="field-selector" id="customization-window">
      <h3>Custom table</h3>
      <div className="fields-container">
        <div className="options-container" style={{ display: "inline-grid" }}>
          {allAvailableFields.map((field) => (
            <label key={field.key} className="option">
              <input
                type="checkbox"
                disabled={necessaryFields?.includes(field.key)}
                checked={selectedFields.includes(field)}
                onChange={() => handleFieldToggle(field)}
              />
              {field.label}
            </label>
          ))}
        </div>
      </div>
      <button className="primary-button" onClick={clearFilters}>
        Clear Filters
      </button>
    </div>
  );
};

export default TableCustomizationWindow;
