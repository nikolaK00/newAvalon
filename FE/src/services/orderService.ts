import { createApi } from "@reduxjs/toolkit/query/react";

import { Order, OrderFormFields } from "../components/order/types";

import config from "./config";
import { ListQueryParams, ListResponse } from "./types";

const orderTagType = "Order";

export const orderApi = createApi({
  ...config,
  reducerPath: "orderApi",
  tagTypes: [orderTagType],
  endpoints: (builder) => ({
    // QUERIES
    getOrders: builder.query<
      { data: Order[]; totalCount: number },
      ListQueryParams
    >({
      query: (params: ListQueryParams) => ({
        url: `/api/order/orders`,
        method: "GET",
        params,
      }),
      transformResponse: (returnValue: ListResponse<Order>) => ({
        data: returnValue.items,
        totalCount: returnValue.totalCount,
      }),
      providesTags: (result) =>
        result
          ? [
              ...result.data.map(({ id }) => ({
                type: "Order" as const,
                id,
              })),
              orderTagType,
            ]
          : [orderTagType],
    }),
    getOrderById: builder.query<Order, string | undefined>({
      query: (id) => `/api/order/orders/${id}`,
      providesTags: (result, error, id) => [{ type: "Order", id }],
    }),
    // MUTATIONS
    addOrder: builder.mutation<Order, OrderFormFields>({
      query: (order: OrderFormFields) => ({
        url: `/api/order/orders`,
        method: "POST",
        body: order,
      }),
      invalidatesTags: [orderTagType],
    }),
    updateOrder: builder.mutation<
      Order,
      { id: string | undefined; order: OrderFormFields }
    >({
      query: ({ id, order }) => ({
        url: `/api/order/orders/${id}`,
        method: "PUT",
        body: order,
      }),
      invalidatesTags: (result, error, arg) => [{ type: "Order", id: arg.id }],
    }),
  }),
});

export const { useGetOrdersQuery } = orderApi;
