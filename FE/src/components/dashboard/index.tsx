import { Outlet } from "react-router-dom";

import Box from "@mui/material/Box";
import Container from "@mui/material/Container";

import Appbar from "../appbar";
export default function Dashboard() {
  return (
    <>
      <Appbar />

      <Container>
        <Box sx={{ my: 10 }}>
          <Outlet />
        </Box>
      </Container>
    </>
  );
}
