import "../../styles/components/auth/Login.scss";
import { FC } from "react";
import { useState, useEffect } from "react";
import { LoginRequest } from "../../models/authModels";
import { FormField } from "./FormField";
import { login } from "../../services/authService";
import { RootState } from "../../store/store";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { Button, Grid2, Typography } from "@mui/material";

export const Login: FC = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  let auth = useSelector((state: RootState) => state.auth);
  const [confirmButtonDisabled, setConfirmButtonDisabled] =
    useState<boolean>(true);

  const [request, setRequest] = useState<LoginRequest>({
    username: "",
    password: "",
  });

  useEffect(() => {
    updateButtonAvailibility();
  }, [request]);

  const updateButtonAvailibility = () => {
    setConfirmButtonDisabled(
      request.username.length < 1 || request.password.length < 1
    );
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setRequest((prevRequest) => ({
      ...prevRequest,
      [name]: value,
    }));
  };

  const handleLogin = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    await login(request, dispatch, navigate);
    auth = useSelector((state: RootState) => state.auth);
  };

  return (
    <Grid2 className="login-container">
      <form className="login-form" onSubmit={handleLogin}>
        <Typography variant="h2">Login</Typography>
        <FormField
          name="username"
          label="Email"
          required
          type="email"
          value={request.username}
          onChange={handleChange}
          placeholder="Email"
          errors={auth?.loginErrors?.errors?.["Username"]}
        />
        <FormField
          name="password"
          label="Password"
          required
          type="password"
          value={request.password}
          onChange={handleChange}
          placeholder="Password"
          errors={auth?.loginErrors?.errors?.["Password"]}
        />
        <Button
          color="secondary"
          variant="contained"
          type="submit"
          disabled={confirmButtonDisabled}
          className="login-button"
        >
          Login
        </Button>
      </form>
      <Grid2 className="login-image">
        <Typography variant="h3">BUILDING THE FUTURE</Typography>
      </Grid2>
    </Grid2>
  );
};
