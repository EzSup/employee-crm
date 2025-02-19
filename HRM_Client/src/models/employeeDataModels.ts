import CustomUserRenderer from "../components/employeeslist/cellRenderers/CustomUserRenderer";
import { Dayjs } from "dayjs";

type EmployeeFieldKey =
  | keyof Employee
  | keyof Hub
  | keyof Person
  | keyof Hirer
  | `${"hub" | "person" | "hirer"}.${string}`;

export interface EntitiesFields {
  key: EmployeeFieldKey;
  label: string;
  width?: number;
  filter?: boolean;
  cellDataType?: string;
  cellRenderer?: any;
  CellRendererParams?: any;
}

export const userColDefinition = {
  field: "user",
  width: 250,
  filter: false,
  cellRenderer: CustomUserRenderer,
  pinned: "left",
  lockPinned: true,
};
export const userFields = [
  "fNameEn",
  "lNameEn",
  "mNameEn",
  "translate",
  "photo",
];

export const EMPLOYEE_AVAILABLE_FIELDS: EntitiesFields[] = [
  { key: "person.personalEmail", label: "Personal email" },
  { key: "techLevel", label: "Technical level", width: 120 },
  { key: "person.gender", label: "Gender", width: 100 },
  { key: "person.englishLevel", label: "English Level" },
  { key: "person.phoneNumber", label: "Phone number", width: 150 },
  { key: "dateOfComing", label: "Date Of Coming", width: 120 },
  { key: "hub.name", label: "Hub name", width: 120 },
  { key: "person.tShirtSize", label: "T-shirt size", width: 120 },
  { key: "person.telegramId", label: "Telegram ID", width: 125 },
  { key: "person.telegramUserName", label: "Telegram Username" },
  { key: "person.birthDate", label: "Birth Date", width: 120 },
  { key: "person.applicationDate", label: "Application Date", width: 170 },
  { key: "hub.leaderName", label: "Leader Name", width: 120 },
  { key: "hub.deputyLeaderName", label: "Deputy Leader Name", width: 120 },
  { key: "mentorName", label: "Mentor", width: 120 },
  { key: "hub.directorName", label: "Director Name", width: 120 },
  { key: "hirer.fullName", label: "Full Name", width: 120 },
  { key: " Mail", label: "  email", width: 120 },
  { key: "hirer.email", label: "HR Email", width: 120 },
];

export const EMPLOYEE_DEFAULT_SELECTED_FIELDS: EntitiesFields[] =
  EMPLOYEE_AVAILABLE_FIELDS.slice(0, 6);

export enum Gender {
  Male = 1,
  Female = 2,
}

export enum EnglishLevel {
  Elementary = 1,
  Pre_Intermediate = 2,
  Intermediate = 3,
  Upper_Intermediate = 4,
  Advanced = 5,
  Proficiency = 6,
}

export const LANGUAGE_LEVELS = [
  { value: EnglishLevel.Elementary, label: "A1" },
  { value: EnglishLevel.Pre_Intermediate, label: "A2" },
  { value: EnglishLevel.Intermediate, label: "B1" },
  { value: EnglishLevel.Upper_Intermediate, label: "B2" },
  { value: EnglishLevel.Advanced, label: "C1" },
  { value: EnglishLevel.Proficiency, label: "C2" },
];

export enum TShirtSize {
  XS = 1,
  S = 2,
  M = 3,
  L = 4,
  XL = 5,
  XXL = 6,
}
export enum TechLEvel {
  Trainee = 1,
  Junior = 2,
  Middle = 3,
  Senior = 4,
  Architect = 5,
  Another = 6,
}

export interface Hirer {
  id: number;
  email: string;
  fullName: string;
}

export interface Hub {
  id: number;
  name: string;
  directorName: string;
  leaderName: string;
  deputyLeaderName: string;
}

export interface Employee {
  id: number;
  techLevel: TechLEvel;
  documents: string[];
  Mail: string;
  mentorName: string;
  person: Person;
  hirer: Hirer;
  hub: Hub;
  dateOfComing: Date;
}

export interface Person {
  id: number;
  fNameEn: string;
  lNameEn: string;
  mNameEn: string;
  gender: Gender;
  englishLevel: EnglishLevel;
  tShirtSize: TShirtSize;
  birthDate: Date;
  hobbies?: string;
  applicationDate: Date;
  phoneNumber?: string;
  personalEmail?: string;
  telegramId: number;
  telegramUserName?: string;
  photo?: string;
  translate: { fNameUk?: string; lNameUk?: string; mNameUk?: string };
}

export interface Form {
  id?: number;
  fNameEn: string;
  lNameEn: string;
  mNameEn: string;
  fNameUk: string;
  lNameUk: string;
  mNameUk: string;
  gender: Gender;
  englishLevel: EnglishLevel;
  tShirtSize: TShirtSize;
  birthDate: Dayjs | null;
  hobbies?: string;
  techStack?: string;
  prevWorkPlace: string | null;
  applicationDate: Dayjs | null;
  phoneNumber?: string;
  personalEmail?: string;
  telegramId: number;
  telegramUserName?: string;
  photo?: string;
  children: Child[] | undefined;
  partner: Partner | null;
}

export interface ChildAndPartnerBase {
  id: number;
  name: string;
  birthDate: Dayjs;
  gender: Gender;
}
export interface Child extends ChildAndPartnerBase {}
export interface Partner extends ChildAndPartnerBase {
  personId: number;
}

export const personFiltersDefaultValue = (): PersonFilters => {
  return {
    gender: [],
    englishLevel: [],
    tShirtSize: [],
  };
};

export interface PersonFilters {
  gender?: Gender[];
  englishLevel: EnglishLevel[];
  tShirtSize: TShirtSize[];
}
