import React, { useState } from "react";

import { LoadingButton } from "@mui/lab";
import { Box, Typography } from "@mui/material";

import {
  useApproveRequestMutation,
  useDisapproveRequestMutation,
} from "../../../services/dealerSevice";
import { useToastMessage } from "../../../shared/hooks/useToastMessage";
import { getUserStatusColor } from "../../../shared/utils";
import { User, UserStatus } from "../types";

export const useUserColumns = (pendingRequests?: boolean) => {
  const [approvingRequestId, setApprovingRequestId] = useState<string | null>();
  const [disapprovingRequestId, setDisapprovingRequestId] = useState<
    string | null
  >();

  const [
    approveRequest,
    {
      isLoading: isApproving,
      isSuccess: isApproveSuccess,
      error: isApproveError,
    },
  ] = useApproveRequestMutation();
  const [
    disaproveRequest,
    {
      isLoading: isDisapproving,
      isSuccess: isDisapproveSuccess,
      error: isDisapproveError,
    },
  ] = useDisapproveRequestMutation();

  // approve toasts
  useToastMessage({
    isSuccess: isApproveSuccess,
    error: isApproveError,
    successMessage: "Request successfully approved",
    errorMessage: "There was a problem when trying to approve a request",
  });

  // disapprove toasts
  useToastMessage({
    isSuccess: isDisapproveSuccess,
    error: isDisapproveError,
    successMessage: "Request successfully disapproved",
    errorMessage: "There was a problem when trying to disapprove a request",
  });

  const RowActions = (row: User) => (
    <Box
      sx={{
        display: "flex",
        alignItems: "center",
        flexDirection: "row",
      }}
    >
      <Typography
        variant="body2"
        sx={{ mr: 3, color: getUserStatusColor(row.status) }}
      >
        {UserStatus[row.status]}
      </Typography>

      {(pendingRequests || row.status === UserStatus.PENDING) && (
        <>
          <LoadingButton
            color={"success"}
            variant={"contained"}
            sx={{ mx: 1, fontSize: 12 }}
            loading={isApproving && approvingRequestId === row.id}
            onClick={() => {
              setApprovingRequestId(row.id);
              approveRequest(row.id);
            }}
          >
            Approve
          </LoadingButton>
          <LoadingButton
            color={"error"}
            sx={{ mx: 1, fontSize: 12 }}
            loading={isDisapproving && disapprovingRequestId === row.id}
            onClick={() => {
              setDisapprovingRequestId(row.id);
              disaproveRequest(row.id);
            }}
          >
            Disapprove
          </LoadingButton>
        </>
      )}
    </Box>
  );

  const userColumns = [
    {
      id: "actions",
      label: "",
      format: (row: User) => RowActions(row),
    },
    {
      id: "firstName",
      label: "First Name",
    },
    {
      id: "lastName",
      label: "Last Name",
    },
    {
      id: "email",
      label: "Email",
    },
  ];

  return userColumns;
};
