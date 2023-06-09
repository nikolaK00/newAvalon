import React, { FC, useEffect } from "react";
import { FormProvider, useForm } from "react-hook-form";
import * as yup from "yup";

import { yupResolver } from "@hookform/resolvers/yup";
import PersonIcon from "@mui/icons-material/Person";
import { Avatar, CircularProgress, Stack, Typography } from "@mui/material";

import { useAddImageMutation } from "../../../services/imageService";
import { useUploadProfileImageMutation } from "../../../services/userService";
import DateInput from "../../../shared/form/DateInput";
import Input from "../../../shared/form/Input";
import SelectInput, { Option } from "../../../shared/form/SelectInput";
import SubmitButton from "../../../shared/form/SubmitButton";
import { useImageUpload } from "../../../shared/hooks/useImageUpload";
import { Role, User, UserFormFields, UserStatus } from "../types";

import {
  address,
  dob,
  email,
  image,
  lastName,
  name,
  password,
  repeatedPassword,
  roles,
  userFormValidation,
  username,
} from "./fields";

const roleOptions: Option[] = [
  { label: "Salesman", value: Role.salesman },
  { label: "Customer", value: Role.customer },
];

interface UserFormProps {
  onSubmit: (data: UserFormFields) => void;
  submitButtonLabel: string;
  formTitle?: string;
  user?: User;
  isSubmitting?: boolean;
}

const UserForm: FC<UserFormProps> = ({
  onSubmit,
  submitButtonLabel,
  formTitle,
  user,
  isSubmitting,
}) => {
  const [addImage, { isLoading: isImageAdding }] = useAddImageMutation();
  const [uploadImage, { isLoading: isImageUploading }] =
    useUploadProfileImageMutation();

  const { handleImageChange, imageSrc, setImageSrc } = useImageUpload({
    addImage,
    uploadImage,
  });

  const schema = yupResolver(
    yup
      .object({
        ...userFormValidation,
        // if user exists (not a register form) password should not be required
        [password]: !user ? yup.string().required() : yup.string(),
        [repeatedPassword]: !user
          ? yup
              .string()
              .oneOf([yup.ref(password)], "Passwords do not match")
              .required()
          : yup.string().oneOf([yup.ref(password)], "Passwords do not match"),
      })
      .required()
  );

  const methods = useForm<UserFormFields>({
    resolver: schema,
    defaultValues: {
      ...user,
      [roles]: user ? +user?.roles : Role.salesman,
    },
    mode: "onSubmit",
  });

  const { handleSubmit } = methods;

  useEffect(() => {
    if (user?.profileImage?.url) {
      setImageSrc(user?.profileImage?.url);
    }
  }, [user?.profileImage?.url]);

  return (
    <>
      {formTitle && <Typography variant="h6">{formTitle}</Typography>}

      {user?.roles === Role.salesman && (
        <Typography variant="body2">({UserStatus[user.status]})</Typography>
      )}

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
          <Input field={username} label={"Username"} />
          <Input field={email} label={"Email"} />
          <Input field={password} label={"Password"} type="password" />
          <Input
            field={repeatedPassword}
            label={"Repeated Password"}
            type="password"
          />
          <Input field={name} label={"Name"} />
          <Input field={lastName} label={"Last Name"} />
          <DateInput field={dob} label={"Date of Birth"} />
          <Input field={address} label={"Address"} />
          <SelectInput
            field={roles}
            label={"Role"}
            options={[
              ...roleOptions,
              ...(user?.roles === Role.admin
                ? [
                    {
                      label: "Admin",
                      value: Role.admin,
                    },
                  ]
                : []),
            ]}
            disabled={!!user}
          />

          {user && (
            <>
              <Avatar
                src={imageSrc}
                alt="User image"
                sx={{
                  width: 150,
                  height: 150,
                  position: "relative",
                  left: "50%",
                  transform: "translate(-50%, 0)",
                }}
              >
                {isImageUploading || isImageAdding ? (
                  <CircularProgress />
                ) : (
                  <PersonIcon fontSize={"large"} />
                )}
              </Avatar>
              <Input
                field={image}
                type={"file"}
                accept={"image/*"}
                label={"Image"}
                onChange={handleImageChange}
              />
            </>
          )}

          <SubmitButton isLoading={isSubmitting}>
            {submitButtonLabel}
          </SubmitButton>
        </Stack>
      </FormProvider>
    </>
  );
};

export default UserForm;
