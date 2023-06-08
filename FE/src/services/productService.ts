import { createApi } from "@reduxjs/toolkit/query/react";

import {
  Product,
  ProductFormFields,
  ProductQueryParams,
} from "../components/product/types";

import config from "./config";
import { ListResponse } from "./types";

const productTagType = "Product";

export const productApi = createApi({
  ...config,
  reducerPath: "productApi",
  tagTypes: [productTagType],
  endpoints: (builder) => ({
    // QUERIES
    getProducts: builder.query<
      { data: Product[]; totalCount: number },
      ProductQueryParams
    >({
      query: (params: ProductQueryParams) => ({
        url: `/api/catalog/product`,
        method: "GET",
        params,
      }),
      transformResponse: (returnValue: ListResponse<Product>) => ({
        data: returnValue.items,
        totalCount: returnValue.totalCount,
      }),
      providesTags: (result) =>
        result
          ? [
              ...result.data.map(({ id }) => ({
                type: "Product" as const,
                id,
              })),
              productTagType,
            ]
          : [productTagType],
    }),
    getProductById: builder.query<Product, string | undefined>({
      query: (id) => `/api/catalog/product/${id}`,
      providesTags: (result, error, id) => [{ type: "Product", id }],
    }),
    // MUTATIONS
    addProduct: builder.mutation<Product, ProductFormFields>({
      query: (product: ProductFormFields) => ({
        url: `/api/catalog/product`,
        method: "POST",
        body: product,
      }),
      invalidatesTags: [productTagType],
    }),
    updateProduct: builder.mutation<
      Product,
      { id: string | undefined; product: ProductFormFields }
    >({
      query: ({ id, product }) => ({
        url: `/api/catalog/product/${id}`,
        method: "PUT",
        body: product,
      }),
      invalidatesTags: (result, error, arg) => [
        { type: "Product", id: arg.id },
      ],
    }),
    deleteProduct: builder.mutation<Product, string | undefined>({
      query: (id) => ({
        url: `/api/catalog/product/${id}`,
        method: "DELETE",
      }),
      invalidatesTags: (result, error, arg) => [{ type: "Product", id: arg }],
    }),
    uploadProductImage: builder.mutation<
      string,
      { imageId: string | undefined; productId?: string | undefined }
    >({
      query: ({ imageId, productId }) => ({
        url: `/api/catalog/product/product-image`,
        method: "PUT",
        body: { imageId, productId },
      }),
    }),
  }),
});

export const {
  useUpdateProductMutation,
  useAddProductMutation,
  useGetProductByIdQuery,
  useGetProductsQuery,
  useDeleteProductMutation,
  useUploadProductImageMutation,
} = productApi;
