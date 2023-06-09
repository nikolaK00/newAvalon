import { FC } from "react";

import {
  Box,
  Divider,
  ListItem,
  ListItemText,
  Typography,
} from "@mui/material";

import { StoreProduct } from "../../../store/cart/cartSlice";

interface CartProductProps {
  product: StoreProduct;
}

const CartProduct: FC<CartProductProps> = ({ product }) => {
  const { name, quantity, price } = product;

  return (
    <Box
      sx={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
      }}
    >
      <ListItem alignItems="flex-start">
        <ListItemText
          primary={name}
          secondary={
            <>
              <Typography
                sx={{ display: "inline" }}
                component="span"
                variant="body2"
                color="text.primary"
              >
                Quantity: {quantity}
              </Typography>
              {` Price: $${price}, 00`}
            </>
          }
        />
      </ListItem>
      <Divider variant="inset" component="li" />
    </Box>
  );
};

export default CartProduct;
