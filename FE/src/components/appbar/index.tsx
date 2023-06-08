import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { NavLink } from "react-router-dom";

import { List, ListItem, ListItemButton, ListItemText } from "@mui/material";
import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";

import { HOME_ROUTE, LOGIN_ROUTE } from "../../routes";
import { RootState } from "../../store";
import { logout } from "../../store/user/userSlice";
import { navItemsProtected, navItemsUnprotected } from "../dashboard/navItems";

export default function Appbar() {
  const dispatch = useDispatch();
  const { roles, isLoggedIn } = useSelector((state: RootState) => state.user);

  const userMenu = isLoggedIn ? navItemsProtected(roles) : navItemsUnprotected;

  const handleLogout = () => {
    localStorage.clear();
    dispatch(logout());
  };

  return (
    <AppBar component="nav">
      <Toolbar>
        <NavLink to={HOME_ROUTE}>
          <Typography variant="h6" component="div">
            NewAvalon
          </Typography>
        </NavLink>

        <Box
          sx={{
            display: "flex",
            justifyContent: "space-between",
            width: "100%",
          }}
        >
          <List sx={{ display: "flex", ml: 3 }}>
            {userMenu.map(({ text, path }) => (
              <ListItem disablePadding sx={{ width: "auto" }} key={path}>
                <NavLink
                  to={path}
                  style={({ isActive }) => ({
                    color: isActive ? "#fff" : "#a4adbd",
                    textDecoration: "none",
                  })}
                >
                  <ListItemButton sx={{ textAlign: "center" }}>
                    <ListItemText primary={text} />
                  </ListItemButton>
                </NavLink>
              </ListItem>
            ))}
          </List>

          {isLoggedIn && (
            <List>
              <ListItem disablePadding sx={{ width: "auto" }}>
                <NavLink
                  to={LOGIN_ROUTE}
                  style={({ isActive }) => ({
                    color: isActive ? "#fff" : "#a4adbd",
                    textDecoration: "none",
                  })}
                  onClick={handleLogout}
                >
                  <ListItemButton sx={{ textAlign: "center" }}>
                    <ListItemText primary={"Logout"} />
                  </ListItemButton>
                </NavLink>
              </ListItem>
            </List>
          )}
        </Box>
      </Toolbar>
    </AppBar>
  );
}
