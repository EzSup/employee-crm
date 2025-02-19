import {FC} from "react";
import {DashboardLayout} from "@toolpad/core/DashboardLayout";
import GlobalSnackbar from "./GlobalSnackbar";
import {Outlet} from "react-router-dom";
import {
  Users,
  CalendarBlank,
  SignOut,
  ChartBar,
  Gear,
  UsersThree,
  Layout as LayoutIcon,
} from "@phosphor-icons/react";
import {AppProvider, Navigation} from "@toolpad/core/AppProvider";
import theme from "../../styles/theme.ts";
import {useRouter} from "../../hooks/useRouter.tsx";
import Usercard from "../auth/Usercard.tsx";
import Header from "./Header.tsx";

const Layout: FC = () => {
  const iconSize = 26;
  const NAVIGATION: Navigation = [
    {
      title: "Dashboard",
      icon: <LayoutIcon size={iconSize}/>,
    },
    {
      segment: "calendar",
      title: "Calendar",
      icon: <CalendarBlank size={iconSize}/>,
    },
    {
      segment: "employees",
      title: "Employees",
      icon: <Users size={iconSize}/>,
    },
    {
      segment: "hubs",
      title: "Hubs",
      icon: <UsersThree size={iconSize}/>,
    },
    {
      segment: "statistics",
      title: "Statistics",
      icon: <ChartBar size={iconSize}/>,
    },
    {
      segment: "settings",
      title: "Settings",
      icon: <Gear size={iconSize}/>,
    },
    {
      segment: "logout",
      title: "Log out",
      icon: <SignOut size={iconSize}/>,
    },
  ];

  const BRANDING = {
    logo: <Header/>,
    title: "",
  };

  const router = useRouter();

  return (
    <AppProvider
      navigation={NAVIGATION}
      branding={BRANDING}
      theme={theme}
      router={router}
    >
      <DashboardLayout
        slots={{
          toolbarActions: Usercard,
        }}
      >
        <GlobalSnackbar/>
        <Outlet/>
      </DashboardLayout>
    </AppProvider>
  );
};

export default Layout;
