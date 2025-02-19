import dayjs, { Dayjs } from "dayjs";
import * as yup from "yup";
import {
	EnglishLevel,
	Form,
	Gender,
	TShirtSize,
} from "../models/employeeDataModels";
import { yupResolver } from "@hookform/resolvers/yup";
import { Resolver } from "react-hook-form";
import { HubCreateRequest, HubUpdateRequest } from "./hubsDataModels";

export const childValidationSchema = yup.object({
	id: yup.number().required("Child ID is required"),
	name: yup.string().required("Child name is required"),
	birthDate: yup
		.mixed<Dayjs>()
		.transform((value) => dayjs(value))
		.required()
		.test(
			"is-valid-date",
			"Birth date must be a valid date",
			(value) => value?.isValid() ?? false
		)
		.test(
			"max-date",
			"Birth date cannot be in the future",
			(value) => value?.isBefore(dayjs()) ?? false
		),
	gender: yup
		.mixed<Gender>()
		.oneOf([Gender.Male, Gender.Female], " Invalid child gender")
		.required("Child gender is required"),
});

export const partnerValidationSchema = yup.object({
	id: yup.number().required(),
	personId: yup.number().required("Partner personId is required"),
	name: yup.string().required("Partner name is required"),
	birthDate: yup
		.mixed<Dayjs>()
		.transform((value, originalValue) => {
			const parsedDate = dayjs(originalValue);
			return parsedDate.isValid() ? parsedDate : null;
		})
		.typeError("Invalid birth date")
		.required("Partner birth date is required")
		.test(
			"min-age",
			"Candidate must be at least 16 years old",
			(value) => value && value.isBefore(dayjs().subtract(16, "years"), "day")
		),
	gender: yup
		.mixed<Gender>()
		.oneOf([Gender.Male, Gender.Female], "Invalid partner gender")
		.required("Partner gender is required"),
});

export const formSchema = yup.object().shape({
	id: yup.number(),
	fNameEn: yup.string().required("First Name (ENG) is required"),
	lNameEn: yup.string().required("Last Name (ENG) is required"),
	mNameEn: yup.string().required("Middle Name (ENG) is required"),
	fNameUk: yup.string().required("First Name (UKR) is required"),
	lNameUk: yup.string().required("Last Name (UKR) is required"),
	mNameUk: yup.string().required("Middle Name (UKR) is required"),
	gender: yup.mixed<Gender>().required("Please specify your gender"),
	englishLevel: yup.mixed<EnglishLevel>().required(),
	tShirtSize: yup.mixed<TShirtSize>().required(),
	birthDate: yup
		.mixed<Dayjs>()
		.nullable()
		.transform((value, originalValue) => {
			const parsedDate = dayjs(originalValue);
			return parsedDate.isValid() ? parsedDate : null;
		})
		.required("Candidate birth date is required")
		.test(
			"min-age",
			"Candidate must be at least 16 years old",
			(value) => value && value.isBefore(dayjs().subtract(16, "years"), "day")
		),
	hobbies: yup.string().optional(),
	techStack: yup.string().optional(),
	prevWorkPlace: yup.string().nullable().default(null),
	applicationDate: yup
		.mixed<Dayjs>()
		.nullable()
		.transform((value, originalValue) => {
			const parsedDate = dayjs(originalValue);
			return parsedDate.isValid() ? parsedDate : null;
		})
		.required("application date is required"),
	phoneNumber: yup
		.string()
		.required("This field is required")
		.matches(
			/^\+?\d+$/,
			"Only digits are allowed, and optional '+' at the beginning"
		),
	personalEmail: yup
		.string()
		.email("Incorrect email pattern")
		.required("This field is required"),
	telegramId: yup.number().required("telegramID is reqiuired"),
	telegramUserName: yup.string().optional(),
	photo: yup.string().optional(),
	children: yup.array().of(childValidationSchema).optional(),
	partner: partnerValidationSchema.nullable(),
});

export const formDefaultValues = {
	resolver: yupResolver(formSchema) as Resolver<Form>,
	defaultValues: {
		fNameEn: "",
		lNameEn: "",
		mNameEn: "",
		fNameUk: "",
		lNameUk: "",
		mNameUk: "",
		gender: Gender.Male,
		englishLevel: EnglishLevel.Elementary,
		tShirtSize: TShirtSize.M,
		birthDate: null,
		applicationDate: null,
		phoneNumber: "",
		personalEmail: "",
		telegramId: 0,
		telegramUserName: "",
		photo: "",
		children: [],
		partner: undefined,
	},
};

export const hubCreateSchema: yup.ObjectSchema<HubCreateRequest> = yup
	.object()
	.shape({
		name: yup.string().required("Hub name is required!"),
		memberIds: yup.array().required("Hub must have any members!"),
		leaderId: yup.number().nullable().optional(),
		deputyLeaderId: yup.number().nullable().optional(),
		directorId: yup.number().nullable().optional(),
	});

export const hubUpdateSchema: yup.ObjectSchema<HubUpdateRequest> = yup
	.object()
	.shape({
		id: yup.number().required(),
		name: yup.string().required("Hub name is required!"),
		memberIds: yup.array().required("Hub must have any members!"),
		leaderId: yup.number().nullable().optional(),
		deputyLeaderId: yup.number().nullable().optional(),
		directorId: yup.number().nullable().optional(),
	});

export const hubCreateDefaultValues = {
	resolver: yupResolver(hubCreateSchema) as Resolver<HubCreateRequest>,
	defaultValues: {
		name: "",
		leaderId: undefined,
		deputyLeaderId: undefined,
		directorId: undefined,
	},
};

export const hubUpdateDefaultValues = {
	resolver: yupResolver(hubUpdateSchema) as Resolver<HubUpdateRequest>,
	defaultValues: {
		id: 0,
		name: "",
	},
};
