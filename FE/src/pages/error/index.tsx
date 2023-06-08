import React, { FC, ReactNode } from "react";

import { CircularProgress, Typography } from "@mui/material";
import Box from "@mui/material/Box";

interface ErrorPageProps {
  error: boolean;
  children: ReactNode;
  isLoading?: boolean;
}
const ErrorPage: FC<ErrorPageProps> = ({ error, children, isLoading }) => {
  if (!error && !isLoading) return <>{children}</>;

  if (isLoading) {
    return <CircularProgress />;
  }

  return (
    <Box
      sx={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        flexDirection: "column",
      }}
    >
      <Typography variant="h3">Oops!</Typography>
      <Typography variant="h6">
        There was an error trying to load the data.
      </Typography>
    </Box>
  );
};

export default ErrorPage;
