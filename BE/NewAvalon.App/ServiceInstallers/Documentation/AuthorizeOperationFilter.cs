﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Globalization;
using System.Linq;
using System.Net;

namespace NewAvalon.App.ServiceInstallers.Documentation
{
    internal class AuthorizeOperationFilter : IOperationFilter
    {
        private static readonly HttpStatusCode[] ResponseStatusCodes =
        {
            HttpStatusCode.Unauthorized,
            HttpStatusCode.Forbidden
        };

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            object[] customAttributes = context.MethodInfo.GetCustomAttributes(true);

            object[] declaringTypeCustomAttributes = context.MethodInfo.DeclaringType!.GetCustomAttributes(true);

            bool isAuthorized = declaringTypeCustomAttributes.OfType<AuthorizeAttribute>().Any() ||
                                customAttributes.OfType<AuthorizeAttribute>().Any();

            bool isAnonymousAllowed = declaringTypeCustomAttributes.OfType<AllowAnonymousAttribute>().Any() ||
                                      customAttributes.OfType<AllowAnonymousAttribute>().Any();

            if (!isAuthorized || isAnonymousAllowed)
            {
                return;
            }

            foreach (HttpStatusCode statusCode in ResponseStatusCodes)
            {
                operation.Responses.TryAdd(
                    ((int)statusCode).ToString(CultureInfo.InvariantCulture),
                    new OpenApiResponse { Description = statusCode.ToString() });
            }
        }
    }
}
