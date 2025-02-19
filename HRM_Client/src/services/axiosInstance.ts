import axios from "axios";
import { showGlobalSnackbar } from "../store/store";

const axiosInstance = axios.create({
  baseURL: "https://localhost:5000/",
  withCredentials: true,
  headers: {
    "Content-Type": "application/json",
  },
});

axiosInstance.interceptors.request.use(
  (config) => {
    return config;
  },
  function (error: Error) {
    console.error(error);
    showGlobalSnackbar(error.message, "warning");
    return Promise.reject(error);
  }
);

axiosInstance.interceptors.response.use(
  (config) => {
    return config;
  },
  function (error: Error) {
    console.error(error);
    showGlobalSnackbar(error.message, "warning");
    return Promise.reject(error);
  }
);

export const isStatusCodeSuccessful = (statusCode: number) =>
  statusCode >= 200 && statusCode < 300;

export default axiosInstance;
