import { createApi } from "@reduxjs/toolkit/query/react";

import config from "./config";

export const dealerTagType = "Dealer";
export const pendingDealerTagType = "PendingDealer";
export const imageTagType = "Image";
export const orderTagType = "Order";
export const productTagType = "Product";

export const api = createApi({
  ...config,
  tagTypes: [
    dealerTagType,
    pendingDealerTagType,
    imageTagType,
    orderTagType,
    productTagType,
  ],
  endpoints: () => ({}),
});
