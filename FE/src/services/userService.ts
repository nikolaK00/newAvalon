import {
  Role,
  User,
  UserCredentials,
  UserFormFields,
} from "../components/user/types";

import { api } from "./service";

interface UserDto extends Omit<User, "roles"> {
  roles: { id: Role }[];
}

export const userApi = api.injectEndpoints({
  endpoints: (builder) => ({
    // QUERIES
    getUser: builder.query<UserDto, void>({
      query: () => `/api/administration/users/me`,
    }),
    // MUTATIONS
    register: builder.mutation<User, Omit<UserFormFields, "repeatedPassword">>({
      query: (user: Omit<UserFormFields, "repeatedPassword">) => ({
        url: `/api/administration/users`,
        method: "POST",
        body: user,
      }),
    }),
    login: builder.mutation<string, UserCredentials>({
      query: (userCredentials: UserCredentials) => ({
        url: `/api/administration/users/login`,
        method: "PUT",
        body: userCredentials,
        responseHandler: "text",
      }),
    }),
    updateUser: builder.mutation<
      User,
      Omit<UserFormFields, "repeatedPassword">
    >({
      query: (user: Omit<UserFormFields, "repeatedPassword">) => ({
        url: `/api/administration/users`,
        method: "PUT",
        body: user,
      }),
    }),
    uploadProfileImage: builder.mutation<string, { imageId: string }>({
      query: (params) => ({
        url: `/api/administration/users/profile-image`,
        method: "PUT",
        body: params,
      }),
    }),
  }),
  overrideExisting: false,
});

export const {
  useGetUserQuery,
  useLazyGetUserQuery,
  useLoginMutation,
  useRegisterMutation,
  useUpdateUserMutation,
  useUploadProfileImageMutation,
} = userApi;
