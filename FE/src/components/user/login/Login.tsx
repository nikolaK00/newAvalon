import React, { useEffect } from "react";
import { FormProvider, useForm } from "react-hook-form";
import { Link, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import * as yup from "yup";

import { yupResolver } from "@hookform/resolvers/yup";
import { Stack, Typography } from "@mui/material";
import Box from "@mui/material/Box";

import { HOME_ROUTE, REGISTER_ROUTE } from "../../../routes";
import {
  useLazyGetUserQuery,
  useLoginMutation,
} from "../../../services/userService";
import Input from "../../../shared/form/Input";
import SubmitButton from "../../../shared/form/SubmitButton";
import { UserCredentials } from "../types";

enum LoginFields {
  email = "email",
  password = "password",
}

const { email, password } = LoginFields;

const schema = yup
  .object({
    [email]: yup.string().email().required(),
    [password]: yup.string().required(),
  })
  .required();

export default function Login() {
  const navigate = useNavigate();

  const [getUser] = useLazyGetUserQuery();

  const [login, { isLoading: isSubmitting, error, isSuccess, data: token }] =
    useLoginMutation();

  const methods = useForm<UserCredentials>({
    resolver: yupResolver(schema),
    mode: "onSubmit",
  });

  const { handleSubmit } = methods;

  const onSubmit = () => {
    login(methods.getValues());
  };

  useEffect(() => {
    if (error) {
      toast.error("There was an error when trying to login");
    }
  }, [error]);

  useEffect(() => {
    if (isSuccess && token) {
      localStorage.setItem("token", token);
      // Result is handled in extraReducers in userSlice and will be added to the store automatically
      getUser();
      toast.success("User logged in successfully");
      navigate(HOME_ROUTE);
    }
  }, [isSuccess, token]);

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
      <FormProvider {...methods}>
        <Stack
          component="form"
          sx={{
            width: "50ch",
          }}
          spacing={2}
          noValidate
          autoComplete="off"
          onSubmit={handleSubmit(onSubmit)}
        >
          <Input field={email} label={"Email"} />
          <Input field={password} label={"Password"} type="password" />

          <SubmitButton isLoading={isSubmitting}>Login</SubmitButton>
        </Stack>
      </FormProvider>

      <Typography variant="h6">Don't have an account?</Typography>
      <Link to={REGISTER_ROUTE}>Register</Link>

      <Box>
        <Typography variant="h6">
          Go to the <Link to={HOME_ROUTE}>Home Page</Link>
        </Typography>
      </Box>
    </Box>
  );
}
