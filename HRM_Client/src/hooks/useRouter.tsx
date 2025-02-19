import { Navigate, Router } from "@toolpad/core";
import { useLocation, useNavigate } from "react-router-dom";

export const useRouter = (): Router => {
	const location = useLocation();
	const navigate = useNavigate();

	const toolpadNavigate: Navigate = (url, options) => {
		const path = typeof url === "string" ? url : url.toString();

		if (options?.history === "replace") {
			navigate(path, { replace: true });
		} else {
			navigate(path);
		}
	};

	return {
		pathname: location.pathname,
		searchParams: new URLSearchParams(location.search),
		navigate: toolpadNavigate,
	};
};
