import React, { FC } from "react";
import { useFormContext } from "react-hook-form";

import {
  FormControl,
  FormHelperText,
  Input as MUIInput,
  InputLabel,
} from "@mui/material";

interface InputProps {
  field: string;
  label: string;
  type?: "password" | "number" | "file";
  accept?: string;
  onChange?: (
    event: React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>
  ) => void;
  multiline?: boolean;
  maxRows?: number;
}

const Input: FC<InputProps> = ({
  field,
  label,
  type = "text",
  accept,
  onChange,
  multiline,
  maxRows,
}) => {
  const {
    register,
    watch,
    formState: { errors },
  } = useFormContext();

  return (
    <FormControl>
      <InputLabel shrink={type === "file" || !!watch(field)} htmlFor={field}>
        {label}
      </InputLabel>
      <MUIInput
        {...register(field, { ...(onChange && { onChange: onChange }) })}
        id={field}
        type={type}
        aria-describedby={`${field}-helper-text`}
        inputProps={{ accept: accept }}
        multiline={multiline}
        maxRows={maxRows}
      />
      {errors[field] && (
        <FormHelperText
          id={`${field}-helper-text`}
          sx={{ color: "red", textTransform: "capitalize" }}
        >
          {errors[field]?.message?.toString()}
        </FormHelperText>
      )}
    </FormControl>
  );
};

export default Input;
