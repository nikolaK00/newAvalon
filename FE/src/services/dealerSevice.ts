import { createApi } from "@reduxjs/toolkit/query/react";

import { User } from "../components/user/types";

import config from "./config";
import { DealerQueryParams, ListResponse } from "./types";

export const dealerTagType = "Dealer";
export const pendingDealerTagType = "PendingDealer";

export const dealerApi = createApi({
  ...config,
  reducerPath: "dealerApi",
  tagTypes: [dealerTagType, pendingDealerTagType],
  endpoints: (builder) => ({
    // QUERIES
    getDealers: builder.query<
      { data: User[]; totalCount: number },
      DealerQueryParams
    >({
      query: ({ pending, ...params }: DealerQueryParams) => ({
        url: `/api/administration/dealer/${pending ? "pending" : ""}`,
        method: "GET",
        params,
      }),
      transformResponse: (returnValue: ListResponse<User>) => ({
        data: returnValue.items,
        totalCount: returnValue.totalCount,
      }),
      providesTags: (result, error, args) =>
        result
          ? [
              ...result.data.map(({ id }) => ({
                type: args.pending
                  ? ("PendingDealer" as const)
                  : ("Dealer" as const),
                id,
              })),
            ]
          : [dealerTagType, pendingDealerTagType],
    }),
    // MUTATIONS
    approveRequest: builder.mutation<User, string | undefined>({
      query: (id) => ({
        url: `/api/administration/dealer/approve/${id}`,
        method: "PUT",
      }),
      invalidatesTags: (result, error, arg) => [
        { type: "Dealer", id: arg },
        { type: "PendingDealer", id: arg },
      ],
    }),
    disapproveRequest: builder.mutation<User, string | undefined>({
      query: (id) => ({
        url: `/api/administration/dealer/disapprove/${id}`,
        method: "PUT",
      }),
      invalidatesTags: (result, error, arg) => [
        { type: "Dealer", id: arg },
        { type: "PendingDealer", id: arg },
      ],
    }),
  }),
});

export const {
  useGetDealersQuery,
  useApproveRequestMutation,
  useDisapproveRequestMutation,
} = dealerApi;
