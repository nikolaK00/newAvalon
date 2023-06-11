import {
  ALL_ORDERS_ROUTE,
  DEALERS_ROUTE,
  LOGIN_ROUTE,
  MY_ORDERS_ROUTE,
  NEW_ORDERS_ROUTE,
  PENDING_REQUESTS_ROUTE,
  PREVIOUS_ORDERS_ROUTE,
  PRODUCTS_ROUTE,
  PROFILE_ROUTE,
  REGISTER_ROUTE,
} from "../../routes";
import { Role } from "../user/types";

const profileNavItem = {
  text: "Profile",
  path: PROFILE_ROUTE,
};
const navItemsAmin = [
  { ...profileNavItem },
  { text: "Pending Requests", path: PENDING_REQUESTS_ROUTE },
  { text: "All Dealers", path: DEALERS_ROUTE },
  { text: "All Orders", path: ALL_ORDERS_ROUTE },
];

const navItemsSales = [
  { ...profileNavItem },
  { text: "Products", path: PRODUCTS_ROUTE },
  { text: "New Orders", path: NEW_ORDERS_ROUTE },
  { text: "My Orders", path: MY_ORDERS_ROUTE },
];

const navItemsCustomer = [
  { ...profileNavItem },
  { text: "Products", path: PRODUCTS_ROUTE },
  { text: "Shipping Orders", path: NEW_ORDERS_ROUTE },
  { text: "Previous Orders", path: PREVIOUS_ORDERS_ROUTE },
];

export const navItemsProtected = (role: Role | null) => {
  const isUserAdmin = role === Role.admin;
  const isUserSalesman = role === Role.salesman;
  const isUserCustomer = role === Role.customer;

  if (isUserAdmin) return navItemsAmin;
  else if (isUserSalesman) return navItemsSales;
  else if (isUserCustomer) return navItemsCustomer;
  return [];
};

export const navItemsUnprotected = [
  { text: "Login", path: LOGIN_ROUTE },
  { text: "Register", path: REGISTER_ROUTE },
];
