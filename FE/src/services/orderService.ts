import {
  Order,
  OrderFormFields,
  OrderResponse,
} from "../components/order/types";

import { api, orderTagType, productTagType } from "./service";
import { ListQueryParams, ListResponse } from "./types";

export const orderService = api.injectEndpoints({
  endpoints: (builder) => ({
    // QUERIES
    getOrders: builder.query<
      { data: OrderResponse[]; totalCount: number },
      ListQueryParams
    >({
      query: (params: ListQueryParams) => ({
        url: `/api/order/orders`,
        method: "GET",
        params,
      }),
      transformResponse: (returnValue: ListResponse<OrderResponse>) => ({
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
      invalidatesTags: [orderTagType, productTagType],
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
      invalidatesTags: (result, error, arg) => [
        { type: "Order", id: arg.id },
        productTagType,
      ],
    }),
  }),
});

export const { useGetOrdersQuery, useAddOrderMutation } = orderService;
