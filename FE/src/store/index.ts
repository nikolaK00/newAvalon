import { configureStore } from "@reduxjs/toolkit";
import { setupListeners } from "@reduxjs/toolkit/query";

import { dealerApi } from "../services/dealerSevice";
import { imageApi } from "../services/imageService";
import { orderApi } from "../services/orderService";
import { productApi } from "../services/productService";
import { userApi } from "../services/userService";

import userReducer from "./user/userSlice";

export const store = configureStore({
  reducer: {
    user: userReducer,
    [userApi.reducerPath]: userApi.reducer,
    [productApi.reducerPath]: productApi.reducer,
    [imageApi.reducerPath]: imageApi.reducer,
    [dealerApi.reducerPath]: dealerApi.reducer,
    [orderApi.reducerPath]: orderApi.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat([
      userApi.middleware,
      productApi.middleware,
      imageApi.middleware,
      dealerApi.middleware,
      orderApi.middleware,
    ]),
});

setupListeners(store.dispatch);

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>;
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch;
