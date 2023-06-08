import { createApi } from "@reduxjs/toolkit/query/react";

import { Entity } from "../shared/types";

import config from "./config";

const imageTagType = "Image";

interface Image extends Entity {
  url: string;
}

export const imageApi = createApi({
  ...config,
  reducerPath: "imageApi",
  tagTypes: [imageTagType],
  endpoints: (builder) => ({
    // QUERIES
    getImageById: builder.query<FormData, string | undefined>({
      query: (id) => `/api/storage/images/${id}`,
      providesTags: (result, error, id) => [{ type: "Image", id }],
    }),
    // MUTATIONS
    addImage: builder.mutation<Image, FormData>({
      query: (image: FormData) => ({
        url: `/api/storage/images`,
        method: "POST",
        body: image,
      }),
      invalidatesTags: [imageTagType],
    }),
  }),
});

export const { useAddImageMutation } = imageApi;
