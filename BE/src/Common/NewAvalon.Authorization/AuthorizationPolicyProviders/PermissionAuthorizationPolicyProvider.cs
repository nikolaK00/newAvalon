﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using NewAvalon.Authorization.Requirements;
using System.Threading.Tasks;

namespace NewAvalon.Authorization.AuthorizationPolicyProviders
{
    /// <summary>
    /// Represents the permission authorization policy provider.
    /// </summary>
    public sealed class PermissionAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionAuthorizationPolicyProvider"/> class.
        /// </summary>
        /// <param name="options">The authorization options.</param>
        public PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
            : base(options)
        {
        }

        /// <inheritdoc />
        public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            AuthorizationPolicy authorizationPolicy = await base.GetPolicyAsync(policyName);

            if (authorizationPolicy is not null)
            {
                return authorizationPolicy;
            }

            var permissionRequirement = new PermissionRequirement(policyName);

            var authorizationPolicyBuilder = new AuthorizationPolicyBuilder();

            authorizationPolicyBuilder.AddRequirements(permissionRequirement);

            AuthorizationPolicy newAuthorizationPolicy = authorizationPolicyBuilder.Build();

            return newAuthorizationPolicy;
        }
    }
}
