import { RouteObject } from "react-router-dom";

import Dashboard from "../components/dashboard";
import OrderList from "../components/order/orderList";
import Product from "../components/product";
import NewProduct from "../components/product/NewProduct";
import ProductList from "../components/product/productList";
import UserProfile from "../components/user/profile/UserProfile";
import { Role } from "../components/user/types";
import UserList from "../components/user/userList";
import HomePage from "../pages/home/HomePage";
import NotFoundPage from "../pages/notFound/NotFoundPage";

import {
  ALL_ORDERS_ROUTE,
  DEALERS_ROUTE,
  HOME_ROUTE,
  MY_ORDERS_ROUTE,
  NEW_ORDERS_ROUTE,
  NEW_PRODUCT_ROUTE,
  PENDING_REQUESTS_ROUTE,
  PREVIOUS_ORDERS_ROUTE,
  PRODUCTS_ROUTE,
  PROFILE_ROUTE,
  ROOT_ROUTE,
} from "./index";

const protectedRoutes = (role: Role | null): RouteObject[] => {
  const isUserAdmin = role === Role.admin;
  const isUserSalesman = role === Role.salesman;
  const isUserCustomer = role === Role.customer;

  const isUserLoggedIn = isUserAdmin || isUserSalesman || isUserCustomer;

  return [
    {
      path: ROOT_ROUTE,
      element: <Dashboard />,
      children: [
        // HOME
        {
          path: ROOT_ROUTE,
          element: <HomePage />,
        },
        {
          path: HOME_ROUTE,
          element: <HomePage />,
        },
        // USER
        {
          ...(isUserLoggedIn && {
            path: PROFILE_ROUTE,
            element: <UserProfile />,
          }),
        },
        // PRODUCT
        {
          ...(isUserSalesman && {
            path: NEW_PRODUCT_ROUTE,
            element: <NewProduct />,
          }),
        },
        {
          ...((isUserSalesman || isUserAdmin || isUserCustomer) && {
            path: PRODUCTS_ROUTE,
            element: <ProductList />,
          }),
        },
        {
          ...((isUserSalesman || isUserAdmin) && {
            path: `${PRODUCTS_ROUTE}/:id`,
            element: <Product />,
          }),
        },
        // ORDER
        {
          ...(isUserCustomer && {
            path: PREVIOUS_ORDERS_ROUTE,
            element: <OrderList />,
          }),
        },
        {
          ...(isUserSalesman && {
            path: NEW_ORDERS_ROUTE,
            element: <OrderList />,
          }),
        },
        {
          ...(isUserSalesman && {
            path: MY_ORDERS_ROUTE,
            element: <OrderList />,
          }),
        },
        {
          ...(isUserAdmin && {
            path: ALL_ORDERS_ROUTE,
            element: <OrderList />,
          }),
        },
        // DEALER
        {
          ...(isUserAdmin && {
            path: PENDING_REQUESTS_ROUTE,
            element: <UserList pendingRequests />,
          }),
        },
        {
          ...(isUserAdmin && {
            path: DEALERS_ROUTE,
            element: <UserList />,
          }),
        },
        // OTHER
        {
          path: "*",
          element: <NotFoundPage />,
        },
      ],
    },
  ];
};

export default protectedRoutes;