import React, { useEffect } from "react";
import { FormProvider, useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";

import { yupResolver } from "@hookform/resolvers/yup";
import { Box, List, Stack, Typography } from "@mui/material";

import { PRODUCTS_ROUTE } from "../../../routes";
import {
  useAddOrderMutation,
  useLazyGetOrderByIdQuery,
} from "../../../services/orderService";
import Input from "../../../shared/form/Input";
import SubmitButton from "../../../shared/form/SubmitButton";
import { useToastMessage } from "../../../shared/hooks/useToastMessage";
import { RootState } from "../../../store";
import { resetCart } from "../../../store/cart/cartSlice";
import {
  comment,
  deliveryAddress,
  orderFormValidation,
} from "../../order/form/fields";
import { OrderFormFields } from "../../order/types";

import CartProduct from "./CartProduct";

const Cart = () => {
  const dispatch = useDispatch();

  const { products: productsInCart } = useSelector(
    (state: RootState) => state.cart
  );

  const [addOrder, { isLoading, isSuccess, error, data }] =
    useAddOrderMutation();
  const [getOrder] = useLazyGetOrderByIdQuery();

  useToastMessage({
    isSuccess,
    error,
    successMessage: "Order placed successfully",
    errorMessage: "There was an error when placing the order",
    successNavigateRoute: PRODUCTS_ROUTE,
  });

  const methods = useForm<OrderFormFields>({
    resolver: yupResolver(orderFormValidation),
    mode: "onSubmit",
  });

  const { handleSubmit, getValues } = methods;

  const onSubmit = () => {
    const orderParams = productsInCart.map((product) => ({
      id: product.id,
      quantity: product.quantity,
    }));
    // placing order and resetting cart
    addOrder({ ...getValues(), products: orderParams });
    dispatch(resetCart());
  };

  useEffect(() => {
    if (isSuccess && data) {
      const newCreatedOrderId = data[0].entityId;
      getOrder(newCreatedOrderId);
    }
  }, [isSuccess, data]);

  return (
    <Box
      sx={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        flexDirection: "column",
      }}
    >
      {!productsInCart?.length ? (
        <Typography variant="h6">Cart is empty</Typography>
      ) : (
        <>
          {productsInCart?.map((product) => (
            <List
              key={product.id}
              sx={{ width: "100%", maxWidth: 360, bgcolor: "background.paper" }}
            >
              <CartProduct key={product.id} product={product} />
            </List>
          ))}
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
              <Input field={comment} label={"Comment"} multiline maxRows={4} />
              <Input field={deliveryAddress} label={"Delivery Address"} />

              <SubmitButton isLoading={isLoading}>Order</SubmitButton>
            </Stack>
          </FormProvider>
        </>
      )}
    </Box>
  );
};

export default Cart;
