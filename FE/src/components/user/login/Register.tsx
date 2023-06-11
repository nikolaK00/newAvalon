import React, { useEffect } from "react";
import { useDispatch } from "react-redux";
import { Link, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

import { Typography } from "@mui/material";
import Box from "@mui/material/Box";
import { CredentialResponse, GoogleLogin } from "@react-oauth/google";

import { HOME_ROUTE, LOGIN_ROUTE } from "../../../routes";
import { api, dealerTagType } from "../../../services/service";
import {
  useLazyGetUserQuery,
  useLoginWithGoogleMutation,
  useRegisterMutation,
} from "../../../services/userService";
import UserForm from "../form";
import { UserFormFields } from "../types";

export default function Register() {
  const navigate = useNavigate();
  const dispatch = useDispatch();

  const [getUser] = useLazyGetUserQuery();
  const [register, { isLoading, error, isSuccess }] = useRegisterMutation();

  const [
    loginWithGoogle,
    {
      isLoading: isSubmitting,
      error: signInWithGoogleError,
      isSuccess: googleSignInSuccess,
      data: tokenWithGoogleLogin,
    },
  ] = useLoginWithGoogleMutation();

  const onSubmit = (data: UserFormFields) => {
    const { repeatedPassword, dateOfBirth, ...dataToSend } = data;
    register({
      ...dataToSend,
      dateOfBirth: new Date(dateOfBirth),
    });
    dispatch(api.util.invalidateTags([dealerTagType]));
  };

  const handleSignInWithGoogle = ({ credential }: CredentialResponse) => {
    loginWithGoogle(credential!);
  };

  useEffect(() => {
    const registerError = error || signInWithGoogleError;
    if (registerError) {
      toast.error("There was an error when trying to register");
    }
  }, [error, signInWithGoogleError]);

  useEffect(() => {
    const registerSuccess = isSuccess || googleSignInSuccess;
    if (registerSuccess) {
      if (tokenWithGoogleLogin) {
        localStorage.setItem("token", tokenWithGoogleLogin);
        getUser();
        navigate(HOME_ROUTE);
      } else {
        navigate(LOGIN_ROUTE);
      }
      toast.success("User registered successfully");
    }
  }, [isSuccess, googleSignInSuccess, tokenWithGoogleLogin]);

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
      <GoogleLogin onSuccess={handleSignInWithGoogle} onError={console.log} />

      <UserForm
        onSubmit={onSubmit}
        submitButtonLabel={"Register"}
        isSubmitting={isLoading || isSubmitting}
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
