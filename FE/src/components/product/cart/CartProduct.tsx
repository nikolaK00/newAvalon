import { FC } from "react";
import { useDispatch } from "react-redux";

import { Box, Button, ListItem, ListItemText, Typography } from "@mui/material";

import { removeFromCart, StoreProduct } from "../../../store/cart/cartSlice";

interface CartProductProps {
  product: StoreProduct;
}

const CartProduct: FC<CartProductProps> = ({ product }) => {
  const { name, quantity, price } = product;

  const dispatch = useDispatch();

  const handleRemoveProductFromCart = () => {
    dispatch(removeFromCart(product.id));
  };

  return (
    <Box
      sx={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
      }}
    >
      <ListItem alignItems="center">
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

        <Button
          variant={"contained"}
          color={"error"}
          onClick={handleRemoveProductFromCart}
        >
          Remove
        </Button>
      </ListItem>
    </Box>
  );
};

export default CartProduct;
