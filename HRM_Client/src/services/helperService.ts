import { Dayjs } from "dayjs";

export const dateToString = (date: Dayjs | null): string => {
	return date && date.isValid() ? date.format("DD/MM/YYYY") : "";
};
