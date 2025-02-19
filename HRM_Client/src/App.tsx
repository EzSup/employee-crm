import { createBrowserRouter, RouterProvider } from "react-router-dom";
import "./styles/globals/App.scss";
import React from "react";
import { Provider } from "react-redux";
import { store, persistor } from "./store/store";
import { PersistGate } from "redux-persist/integration/react";
import { ApolloClient, ApolloProvider, InMemoryCache } from "@apollo/client";
import { LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import "dayjs/locale/uk";
import PrivateRoute from "./components/auth/PrivateRoute";
import Layout from "./components/layout/Layout";
import LogOut from "./components/auth/LogOut";
import Dashboard from "./views/Dashboard";
import EmployeesList from "./views/EmployeesList";
import { ThemeProvider } from "@mui/material";
import theme from "./styles/theme";
import PageNotFound from "./views/PageNotFound";
import LoginPage from "./views/LoginPage";
import EmployeeFormPage from "./views/EmployeeFormPage";
import HubsList from "./views/HubsList.tsx";
import HubCreate from "./views/HubCreate";
import HubManage from "./views/HubManage.tsx";

const App: React.FC = () => {
  const client = new ApolloClient({
    uri: "https://localhost:5000/graphql/",
    cache: new InMemoryCache(),
    credentials: "include",
  });

  const router = createBrowserRouter([
    {
      path: "/login",
      Component: LoginPage,
    },
    {
      Component: PrivateRoute,
      children: [
        {
          path: "/",
          Component: Layout,
          children: [
            {
              path: "/logout",
              Component: LogOut,
            },
            {
              path: "",
              Component: Dashboard,
            },
            {
              path: "employees",
              Component: EmployeesList,
            },
            {
              path: "hubs",
              Component: HubsList,
            },
            {
              path: "hubCreate",
              Component: HubCreate,
            },
            {
              path: "hubManage/:id",
              Component: HubManage,
            },
            {
              path: "employeeForm/:id",
              Component: EmployeeFormPage,
            },
            {
              path: "*",
              Component: PageNotFound,
            },
          ],
        },
      ],
    },
  ]);

  return (
    <div className="App">
      <ApolloProvider client={client}>
        <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale="uk">
          <Provider store={store}>
            <PersistGate persistor={persistor}>
              <ThemeProvider theme={theme}>
                <RouterProvider router={router} />
              </ThemeProvider>
            </PersistGate>
          </Provider>
        </LocalizationProvider>
      </ApolloProvider>
    </div>
  );
};

export default App;
