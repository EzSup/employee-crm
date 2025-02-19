import { FC, useEffect, useState } from "react";
import {
  createEmployeesQuery,
  searchEmployeeByName,
} from "../services/employeesSevice";
import { useQuery } from "@apollo/client";
import {
  EMPLOYEE_AVAILABLE_FIELDS,
  EMPLOYEE_DEFAULT_SELECTED_FIELDS,
  Employee,
  EntitiesFields,
  PersonFilters,
  personFiltersDefaultValue,
  userColDefinition,
  userFields,
} from "../models/employeeDataModels";
import "../styles/view/EmployeesList.scss";
import { AgGridReact } from "ag-grid-react";
import "ag-grid-community/styles/ag-grid.css";
import "ag-grid-community/styles/ag-theme-quartz.css";
import TableCustomizationWindow from "../components/toolsbar/TableCustomizationWindow";
import TableFiltrationWindow from "../components/employeeslist/TableFiltrationWindow";
import ToolsBar from "../components/toolsbar/ToolsBar";
import { exportExcel } from "../services/exportService";

export const EmployeesList: FC = () => {
  const [customTableVisible, setCustomTableVisible] = useState(false);
  const [personSearchedName, setPersonSearchedName] = useState("");
  const [personFilter, setPersonFilter] = useState<PersonFilters>(
    personFiltersDefaultValue()
  );

  const [selectedEmployeeFields, setEmployeeFields] = useState<
    EntitiesFields[]
  >(EMPLOYEE_DEFAULT_SELECTED_FIELDS);

  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [localError, setLocalError] = useState<Error | null>(null);

  const [rowData, setRowData] = useState<Employee[]>([]);
  const [colDefs, setColDefs] = useState<any[]>([]);

  const query = createEmployeesQuery(selectedEmployeeFields, personFilter);
  const queryString = query.loc?.source.body;

  const { loading, error, data } = useQuery(query);

  useEffect(() => {
    setIsLoading(loading);
    if (error) setLocalError(error);
    if (data) {
      setColDefs([
        userColDefinition,
        ...selectedEmployeeFields
          .filter((x) => !userFields.includes(x.key))
          .map((x) => {
            return {
              field: x.key,
              width: x.width,
              filter: x.filter,
              cellDataType: x.cellDataType,
            };
          }),
      ]);

      const filtered = searchEmployeeByName(
        personSearchedName,
        data.employees.nodes
      );
      const allData = filtered.map((node: Employee) => ({
        ...node,
        birthDate: node.person.birthDate
          ? new Date(node.person.birthDate)
          : null,
        applicationDate: node.person.applicationDate
          ? new Date(node.person.applicationDate)
          : null,
        user: {
          fNameEn: node.person.fNameEn,
          lNameEn: node.person.lNameEn,
          fNameUk: node.person.translate.fNameUk,
          lNameUk: node.person.translate.lNameUk,
          photo: node.person.photo,
        },
      }));
      setRowData(allData);
    }
  }, [loading, error, data, personSearchedName, personFilter]);

  return (
    <div className="page">
      <ToolsBar
        customTableVisible={customTableVisible}
        onCustomTableVisibilityChange={setCustomTableVisible}
        onNameFilterUpdate={setPersonSearchedName}
        searchBarPlaceholder="Search employee"
        downloadAction={() => exportExcel(queryString ?? "")}
      />
      {customTableVisible && (
        <div className="all-filters-container">
          <TableCustomizationWindow
            allAvailableFields={EMPLOYEE_AVAILABLE_FIELDS}
            selectedFields={selectedEmployeeFields}
            onSelectedFieldsChange={setEmployeeFields}
          />
          <TableFiltrationWindow
            filters={personFilter}
            onFiltersChange={setPersonFilter}
          />
        </div>
      )}
      {localError && <p>Error: {localError.message}</p>}
      <div className="ag-theme-quartz" style={{ height: "80vh" }}>
        <AgGridReact
          rowData={rowData}
          columnDefs={colDefs}
          defaultColDef={{ filter: true, floatingFilter: false }}
          rowHeight={60}
          pagination={true}
          paginationAutoPageSize
          alwaysShowHorizontalScroll={true}
          paginationPageSize={20}
          loading={isLoading}
        />
      </div>
    </div>
  );
};

export default EmployeesList;
