import { showGlobalSnackbar } from "../store/store";
import axiosInstance, { isStatusCodeSuccessful } from "./axiosInstance";

export const exportExcel = async (queryString: string) => {
  showGlobalSnackbar("Awaiting for document to generate...", "info");
  var response = await axiosInstance.post(`export/toExcel`, queryString, {
    responseType: "blob",
  });

  if (isStatusCodeSuccessful(response.status)) {
    downloadFile(response.data);
    showGlobalSnackbar("Successfully uploaded!", "success");
  } else {
    showGlobalSnackbar("An error occured while processing.", "error");
  }
};

const downloadFile = (
  data: any,
  name: string = `Export-${new Date().toISOString().split("T")[0]}.xlsx`
) => {
  var blob = new Blob([data]);
  const url = window.URL.createObjectURL(blob);
  const link = document.createElement("a");
  link.href = url;

  link.setAttribute("download", name);

  document.body.appendChild(link);
  link.click();

  link.remove();
  window.URL.revokeObjectURL(url);
};
