import { createSlice } from "@reduxjs/toolkit";

import { Product } from "../../components/product/types";

export interface StoreProduct extends Product {
  quantity: number;
}

export type CartState = {
  products: StoreProduct[];
};

const initialState: CartState = {
  products: [],
};

export const cartSlice = createSlice({
  name: "cart",
  initialState,
  reducers: {
    addToCart: (state, action) => {
      const addedProductId = action.payload.id;
      const productAlreadyInCart = state.products.find(
        (product) => product.id === addedProductId
      );
      if (productAlreadyInCart) {
        state.products = state.products.map((product) => {
          if (product.id === addedProductId) {
            const newQuantity = product.quantity + action.payload.quantity;
            console.log(newQuantity);
            return { ...product, quantity: newQuantity };
          } else return product;
        });
      } else {
        state.products = [...state.products, action.payload];
      }
    },
    removeFromCart: (state, action) => {
      const productToDelete = state.products.find(
        (product) => product.id === action.payload
      );
      if (productToDelete) {
        const productIndex = state.products.indexOf(productToDelete);
        state.products = state.products.splice(productIndex, 1);
      }
    },
    resetCart: () => initialState,
  },
});

export const { addToCart, removeFromCart, resetCart } = cartSlice.actions;

export default cartSlice.reducer;
