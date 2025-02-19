import axiosInstance from "./axiosInstance";

export const getTotalEmployeesCount = async (): Promise<number> => {
	let result = 0;
	await axiosInstance
		.get<number>("/api/statistics/totalEmployeesCount")
		.then((response) => {
			result = response.data;
		})
		.catch((error) => {
			alert(error);
		});
	return result;
};
