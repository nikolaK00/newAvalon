import React from "react";

import { Typography } from "@mui/material";
import Paper from "@mui/material/Paper";

export default function HomePage() {
  return (
    <Paper
      sx={{
        height: "100vh",
        width: "100vw",
        position: "absolute",
        left: 0,
        top: 0,
        backgroundImage:
          "url(https://thehill.com/wp-content/uploads/sites/2/2022/08/GettyImages-1133980246.jpg?strip=1)",
      }}
    >
      <Typography
        variant="h3"
        sx={{
          position: "absolute",
          top: "50%",
          left: "50%",
          transform: "translate(-50%, -50%)",
          color: "white",
          fontWeight: 600,
        }}
      >
        NewAvalon shopping
      </Typography>
    </Paper>
  );
}
