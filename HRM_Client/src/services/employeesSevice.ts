import { gql } from "@apollo/client";
import {
  Gender,
  EnglishLevel,
  TShirtSize,
  Person,
  Form,
  Partner,
  Employee,
} from "../models/employeeDataModels";
import { PersonFilters } from "../models/employeeDataModels";
import axiosInstance from "./axiosInstance";
import { EntitiesFields } from "../models/employeeDataModels";

export const createEmployeesQuery = (
  selectedEmployeeFields: EntitiesFields[],
  filters?: PersonFilters
) => {
  const personFields = selectedEmployeeFields
    .filter((field) => field.key.startsWith("person"))
    .map((field) => field.key.replace("person.", ""))
    .join(" ");

  const hirerFields = selectedEmployeeFields
    .filter((field) => field.key.startsWith("hirer"))
    .map((field) => field.key.replace("hirer.", ""))
    .join(" ");

  const hubFields = selectedEmployeeFields
    .filter((field) => field.key.startsWith("hub"))
    .map((field) => field.key.replace("hub.", ""))
    .join(" ");

  const employeeFields = selectedEmployeeFields
    .filter(
      (field) =>
        !field.key.startsWith("person") &&
        !field.key.startsWith("hirer") &&
        !field.key.startsWith("hub")
    )
    .map((field) => field.key)
    .join(" ");

  return gql`
      query {
        employees(where: {
          ${filters ? applyFilters(filters) : ""}
        }) {
          nodes {
            id
            person {
              id
              fNameEn
              lNameEn
              mNameEn
              photo
              translate {
                fNameUk
                lNameUk
                mNameUk
              }
			 ${personFields ? personFields : ""}	
            }
		
			${hirerFields ? `hirer {id ${hirerFields} }` : ""}
            dateOfComing
            
			${hubFields ? `hub {id ${hubFields} }` : ""} 
      ${employeeFields}    
      }
          pageInfo {
            hasNextPage
            endCursor
          }
        }
      }
    `;
};

const applyFilters = (filter: PersonFilters) => {
  return `and: [
    ${
      filter.gender && filter.gender?.length > 0
        ? `{ or: [${filter.gender
            .map(
              (genFilter) =>
                `{person:{ gender: { eq: ${Gender[
                  genFilter
                ].toUpperCase()} } }}`
            )
            .join(", ")}] }`
        : ""
    }    
    ${
      filter.englishLevel && filter.englishLevel?.length > 0
        ? `{ or: [${filter.englishLevel
            .map(
              (genFilter) =>
                `{person:{ englishLevel: { eq: ${EnglishLevel[
                  genFilter
                ].toUpperCase()} } } }`
            )
            .join(", ")}] }`
        : ""
    }
    ${
      filter.tShirtSize && filter.tShirtSize?.length > 0
        ? `{ or: [${filter.tShirtSize
            .map(
              (genFilter) =>
                `{ person:{ tShirtSize: { eq: ${TShirtSize[
                  genFilter
                ].toUpperCase()} } } }`
            )
            .join(", ")}] }`
        : ""
    }  
]`;
};

export const searchEmployeeByName = (
  searchValue: string,
  array: Employee[]
): Employee[] => {
  const normalizedValue = searchValue.toLowerCase();
  return array.filter(
    (x: Employee) =>
      x.person.fNameEn.toLowerCase().includes(normalizedValue) ||
      x.person.lNameEn.toLowerCase().includes(normalizedValue) ||
      x.person.mNameEn.toLowerCase().includes(normalizedValue) ||
      x.person.translate.fNameUk?.toLowerCase().includes(normalizedValue) ||
      x.person.translate.lNameUk?.toLowerCase().includes(normalizedValue) ||
      x.person.translate.mNameUk?.toLowerCase().includes(normalizedValue)
  );
};

export const getNotAcceptedForms = async (): Promise<Person[]> => {
  let data: Person[] = [];
  await axiosInstance
    .get<Person[]>("/api/applications/notApprovedForms")
    .then((response) => {
      data = response.data;
    });
  return data;
};

export const getFormById = async (id: number): Promise<Form> => {
  let data: Form;
  await axiosInstance
    .get<Form>(`/api/applications/form/${id}`)
    .then((response) => {
      data = response.data;
    });
  return data!;
};

export const refuseForm = async (id: number) =>
  await axiosInstance.delete(`/api/applications/reject/${id}`);

export const approveForm = async (id: number) =>
  await axiosInstance.post(`/api/applications/approveForm/${id}`);

export const getCV = async (id: number): Promise<string | undefined> => {
  let result = undefined;
  await axiosInstance.get(`/api/applications/cv/${id}`).then((response) => {
    result = response.data;
  });
  return result;
};

export const getPassportScan = async (
  id: number
): Promise<string | undefined> => {
  let result = undefined;
  await axiosInstance
    .get(`/api/applications/passportScan/${id}`)
    .then((response) => {
      result = response.data;
    });
  return result;
};

export const updateAndApproveForm = async (request: Form) => {
  if (await updateForm(request)) await approveForm(request.id!);
};

export const updateForm = async (request: Form): Promise<boolean> => {
  let result = false;
  await axiosInstance
    .put(`/api/applications/form/${request.id}`, request, {
      withCredentials: true,
      headers: {
        "Content-Type": "application/json",
      },
    })
    .then((response) => {
      result = response.status === 200;
    });
  return result;
};

export const deletePartner = async (id: number) => {
  await axiosInstance.delete(`/api/partners/${id}`);
};

export const createPartner = async (request: Partner) => {
  await axiosInstance.post(`/api/partners/`, request);
};

export const deleteChild = async (id: number) => {
  await axiosInstance.delete(`/api/children/${id}`);
};
