import { useEffect } from "react";
import { useSelector } from "react-redux";
import { useRoutes } from "react-router-dom";

import { Box, CircularProgress } from "@mui/material";

import ErrorPage from "./pages/error";
import protectedRoutes from "./routes/protectedRoutes";
import unprotectedRoutes from "./routes/unprotectedRoutes";
import { useLazyGetUserQuery } from "./services/userService";
import { RootState } from "./store";

import "./App.css";

function App() {
  const [getUser, { isError }] = useLazyGetUserQuery();

  const user = useSelector((state: RootState) => state.user);

  const isLoggedIn = user?.isLoggedIn;
  const token = localStorage.getItem("token");

  const routes = useRoutes(
    isLoggedIn ? protectedRoutes(user?.roles) : unprotectedRoutes
  );

  useEffect(() => {
    if (token && !isLoggedIn) {
      getUser();
    }
  }, []);

  if (token && !isLoggedIn) {
    return (
      <ErrorPage error={isError}>
        <Box
          sx={{
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
            flexDirection: "column",
            minHeight: "100vh",
          }}
        >
          <CircularProgress />
        </Box>
      </ErrorPage>
    );
  }

  return routes;
}

export default App;
