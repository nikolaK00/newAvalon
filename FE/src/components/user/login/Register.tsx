import React, { useEffect } from "react";
import { useDispatch } from "react-redux";
import { Link, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

import { Typography } from "@mui/material";
import Box from "@mui/material/Box";

import { HOME_ROUTE, LOGIN_ROUTE } from "../../../routes";
import { dealerApi, dealerTagType } from "../../../services/dealerSevice";
import { useRegisterMutation } from "../../../services/userService";
import UserForm from "../form";
import { UserFormFields } from "../types";

export default function Register() {
  const navigate = useNavigate();
  const dispatch = useDispatch();

  const [register, { isLoading: isSubmitting, error, isSuccess }] =
    useRegisterMutation();
  const onSubmit = (data: UserFormFields) => {
    const { repeatedPassword, dateOfBirth, ...dataToSend } = data;
    register({
      ...dataToSend,
      dateOfBirth: new Date(dateOfBirth),
    });
    dispatch(dealerApi.util.invalidateTags([dealerTagType]));
  };

  useEffect(() => {
    if (error) {
      toast.error("There was an error when trying to register");
    }
  }, [error]);

  useEffect(() => {
    if (isSuccess) {
      navigate(LOGIN_ROUTE);
      toast.success("User registered successfully");
    }
  }, [isSuccess]);

  return (
    <Box
      sx={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        flexDirection: "column",
        minHeight: "100vh",
      }}
    >
      <UserForm
        onSubmit={onSubmit}
        submitButtonLabel={"Register"}
        isSubmitting={isSubmitting}
      />

      <Typography variant="h6">Already have an account?</Typography>
      <Link to={LOGIN_ROUTE}>Login</Link>

      <Box>
        <Typography variant="h6">
          Go to the <Link to={HOME_ROUTE}>Home Page</Link>
        </Typography>
      </Box>
    </Box>
  );
}
