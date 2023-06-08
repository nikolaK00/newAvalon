import React, { FC } from "react";
import { FormProvider, useForm } from "react-hook-form";

import { yupResolver } from "@hookform/resolvers/yup";
import FolderIcon from "@mui/icons-material/Folder";
import { Avatar, CircularProgress, Stack, Typography } from "@mui/material";
import Box from "@mui/material/Box";

import { useAddImageMutation } from "../../../services/imageService";
import { useUploadProductImageMutation } from "../../../services/productService";
import Input from "../../../shared/form/Input";
import SubmitButton from "../../../shared/form/SubmitButton";
import { useImageUpload } from "../../../shared/hooks/useImageUpload";
import { Product, ProductFormFields } from "../types";

import {
  amount,
  description,
  image,
  name,
  price,
  productFormValidation,
} from "./fields";

interface ProductFormProps {
  onSubmit: (data: ProductFormFields) => void;
  submitButtonLabel: string;
  formTitle?: string;
  product?: Product;
  isSubmitting?: boolean;
}

const ProductForm: FC<ProductFormProps> = ({
  submitButtonLabel,
  onSubmit,
  formTitle,
  isSubmitting,
  product,
}) => {
  const [addImage, { isLoading: isImageAdding }] = useAddImageMutation();
  const [uploadImage, { isLoading: isImageUploading }] =
    useUploadProductImageMutation();

  const { handleImageChange, imageSrc } = useImageUpload({
    addImage,
    uploadImage,
    productId: product?.id,
  });

  const methods = useForm<ProductFormFields>({
    resolver: yupResolver(productFormValidation),
    defaultValues: product,
    mode: "onSubmit",
  });

  const { handleSubmit } = methods;

  return (
    <Box
      sx={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        flexDirection: "column",
      }}
    >
      {formTitle && <Typography variant="h6">{formTitle}</Typography>}

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
          <Input field={name} label={"Name"} />
          <Input field={price} label={"Price"} type="number" />
          <Input field={amount} label={"Amount"} type="number" />
          <Input
            field={description}
            label={"Description"}
            multiline
            maxRows={4}
          />

          <Avatar
            src={imageSrc}
            variant={"square"}
            alt="Product image"
            sx={{
              width: 350,
              height: 250,
              position: "relative",
              left: "50%",
              transform: "translate(-50%, 0)",
            }}
          >
            {isImageUploading || isImageAdding ? (
              <CircularProgress />
            ) : (
              <FolderIcon fontSize={"large"} />
            )}
          </Avatar>

          <Input
            field={image}
            type={"file"}
            accept={"image/*"}
            label={"Image"}
            onChange={handleImageChange}
          />

          <SubmitButton isLoading={isSubmitting}>
            {submitButtonLabel}
          </SubmitButton>
        </Stack>
      </FormProvider>
    </Box>
  );
};

export default ProductForm;
