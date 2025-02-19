import { gql } from "@apollo/client";
import { EntitiesFields, Hub } from "../models/employeeDataModels";
import axiosInstance, { isStatusCodeSuccessful } from "./axiosInstance";
import {
	HubCreateRequest,
	HubMember,
	HubResponse,
	HubUpdateRequest,
} from "../models/hubsDataModels";
import { showGlobalSnackbar } from "../store/store";

export const createHubsQuery = (selectedHubFields: EntitiesFields[]) => {
	const selectedFieldsString =
		selectedHubFields?.map((x) => x.key.toString()).join(" ") || "";
	return gql`
    query {
      hubs {
        nodes {
          id
          name
          ${selectedFieldsString}
        }
      }
    }
  `;
};

export const searchHubByName = (searchValue: string, array: Hub[]): Hub[] => {
	const normalizedValue = searchValue.toLowerCase();
	return array.filter((x: Hub) =>
		x.name.toLowerCase().includes(normalizedValue)
	);
};

export const getFreeEmployees = async (): Promise<HubMember[]> => {
	const result = await axiosInstance.get("api/hub/freeEmployees");
	return result.data;
};

export const postHub = async (request: HubCreateRequest): Promise<boolean> => {
	const result = await axiosInstance.post("api/hub", request);
	if (isStatusCodeSuccessful(result.status)) {
		showGlobalSnackbar(
			`Hub with id ${result.data} successfully created!`,
			"success"
		);
		return true;
	}
	return false;
};

export const putHub = async (request: HubUpdateRequest): Promise<boolean> => {
	const result = await axiosInstance.put("api/hub", request);
	if (isStatusCodeSuccessful(result.status)) {
		showGlobalSnackbar(
			`Hub with id ${result.data} successfully updated!`,
			"success"
		);
		return true;
	}
	return false;
};

export const deleteHub = async (id: number): Promise<boolean> => {
	const result = await axiosInstance.delete(`api/hub/${id}`);
	if (isStatusCodeSuccessful(result.status)) {
		showGlobalSnackbar(`Hub with id ${id} successfully deleted!`, "success");
		return true;
	}
	return false;
};

export const getHub = async (id: number): Promise<HubResponse> => {
	const result = await axiosInstance.get(`api/hub/${id}`);
	return result.data;
};
