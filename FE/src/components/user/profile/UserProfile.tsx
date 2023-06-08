import React from "react";

import { Typography } from "@mui/material";
import Box from "@mui/material/Box";

import ErrorPage from "../../../pages/error";
import {
  useGetUserQuery,
  useUpdateUserMutation,
} from "../../../services/userService";
import UserForm from "../form";
import { Role, UserFormFields } from "../types";

const UserProfile = () => {
  const { data, error, isLoading } = useGetUserQuery();

  const [updateProfile, { isLoading: isSubmitting }] = useUpdateUserMutation();

  const onSubmit = (data: Omit<UserFormFields, "repeatedPassword">) =>
    updateProfile(data);

  return (
    <Box
      sx={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        flexDirection: "column",
      }}
    >
      <ErrorPage error={!!error} isLoading={isLoading}>
        <Typography variant="h6">My Profile</Typography>
        <UserForm
          onSubmit={onSubmit}
          submitButtonLabel={"Edit"}
          user={data && { ...data, roles: data.roles[0].id as Role }}
          isSubmitting={isSubmitting}
        />
      </ErrorPage>
    </Box>
  );
};

export default UserProfile;
