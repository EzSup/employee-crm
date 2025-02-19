import { EntitiesFields } from "./employeeDataModels";

export const HUB_AVAILABLE_FIELDS: EntitiesFields[] = [
	{ key: "name", label: "Hub name", width: 100 },
	{ key: "directorName", label: "Director", width: 250 },
	{ key: "leaderName", label: "Leader", width: 250 },
	{ key: "deputyLeaderName", label: "Deputy Leader", width: 250 },
];

export interface HubMember {
	id: number;
	fullName: string;
	techLevel?: string;
	techStack?: string;
}

export const toShortData = (input: HubMember): string => {
	return ` ${input.id} | ${input.fullName} | ${input.techLevel} | ${input.techStack?.split(",")[0] || "-"}`;
};

export interface HubCreateRequest {
	name: string;
	memberIds: number[];
	leaderId?: number | null;
	deputyLeaderId?: number | null;
	directorId?: number | null;
}

export interface HubUpdateRequest extends HubCreateRequest {
	id: number;
}

export interface HubResponse {
	id: number;
	name: string;
	directorId: number;
	leaderId: number;
	deputyLeaderId: number;
	directorName: string;
	leaderName: string;
	deputyLeaderName: string;
	employees: HubMember[];
}

export const userFields = ["id", "name"];
