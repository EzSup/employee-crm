export interface User {
	isAuthenticated: boolean;
	username: string | null;
	fullName: string | null;
}

export interface AuthContextType {
	user: User;
	loginErrors: LoginErrors | null;
	refreshTerm: Date | null;
}

export interface LoginRequest {
	username: string;
	password: string;
}

export interface GetDataResponse {
	fullName: string;
	username: string;
}

export interface LoginErrors {
	errors: {
		[key: string]: string[];
	};
}
