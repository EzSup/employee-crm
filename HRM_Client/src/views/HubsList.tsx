import { FC, useEffect, useState } from "react";
import { AgGridReact } from "ag-grid-react";
import ToolsBar from "../components/toolsbar/ToolsBar.tsx";
import { EntitiesFields, Hub } from "../models/employeeDataModels.ts";
import {
  HUB_AVAILABLE_FIELDS,
  userFields as hubNecessaryFields,
} from "../models/hubsDataModels.ts";
import TableCustomizationWindow from "../components/toolsbar/TableCustomizationWindow.tsx";
import {
  createHubsQuery,
  deleteHub,
  searchHubByName,
} from "../services/hubsService.ts";
import { useQuery } from "@apollo/client";
import { exportExcel } from "../services/exportService.ts";
import ConfirmationDialog from "../components/layout/ConfirmationDialog.tsx";

const HubsList: FC = () => {
  const [customTableVisible, setCustomTableVisible] = useState<boolean>(false);
  const [hubSearchedName, setHubSearchedName] = useState<string>("");
  const [selectedObject, setSelectedObject] = useState<Hub | undefined>();

  const [deleteDialogOpen, setDeleteDialogOpen] = useState<boolean>(false);

  const [selectedHubFields, setHubFields] =
    useState<EntitiesFields[]>(HUB_AVAILABLE_FIELDS);

  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [localError, setLocalError] = useState<Error | null>(null);

  const [rowData, setRowData] = useState<Hub[]>([]);
  const [colDefs, setColDefs] = useState<any[]>([]);

  const query = createHubsQuery(selectedHubFields);
  const queryString = query.loc?.source.body;

  const { loading, error, data } = useQuery(query);

  useEffect(() => {
    setIsLoading(loading);
    if (error) setLocalError(error);
    if (data) {
      setColDefs([
        ...selectedHubFields.map((x) => {
          return {
            field: x.key,
            width: x.width,
            filter: x.filter,
            cellDataType: x.cellDataType,
          };
        }),
      ]);
      const filtered = searchHubByName(hubSearchedName, data.hubs.nodes);
      setRowData(filtered);
    }
  }, [loading, error, data, hubSearchedName]);

  return (
    <div className="page">
      {deleteDialogOpen && (
        <ConfirmationDialog
          title={`Are you sure to delete ${selectedObject?.name}?`}
          onConfirm={() => {
            if (selectedObject?.id) deleteHub(selectedObject?.id);
          }}
          onRefuse={() => setDeleteDialogOpen(false)}
        />
      )}
      <ToolsBar
        customTableVisible={customTableVisible}
        onCustomTableVisibilityChange={setCustomTableVisible}
        onNameFilterUpdate={setHubSearchedName}
        searchBarPlaceholder="Search hub"
        downloadAction={() => exportExcel(queryString ?? "")}
        {...(selectedObject
          ? {
              deleteAction: () => setDeleteDialogOpen(true),
              editLink: `/hubManage/${selectedObject?.id}`,
            }
          : {})}
        createLink="/hubCreate"
      />
      {customTableVisible && (
        <div className="all-filters-container">
          <TableCustomizationWindow
            allAvailableFields={HUB_AVAILABLE_FIELDS}
            selectedFields={selectedHubFields}
            onSelectedFieldsChange={setHubFields}
            necessaryFields={hubNecessaryFields}
          />
        </div>
      )}
      {localError && <p>Error: {localError.message}</p>}
      <div className="ag-theme-quartz" style={{ height: "80vh" }}>
        <AgGridReact
          onRowClicked={(selected) => setSelectedObject(selected.data)}
          rowData={rowData}
          columnDefs={colDefs}
          defaultColDef={{ filter: true, floatingFilter: false }}
          rowHeight={60}
          pagination={true}
          alwaysShowHorizontalScroll={true}
          paginationPageSize={20}
          loading={isLoading}
        />
      </div>
    </div>
  );
};

export default HubsList;
