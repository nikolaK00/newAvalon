﻿using Microsoft.AspNetCore.Authorization;

namespace NewAvalon.Authorization.Requirements
{
    /// <summary>
    /// Represents the permission authorization requirement.
    /// </summary>
    public sealed class PermissionRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionRequirement"/> class.
        /// </summary>
        /// <param name="permissionName">The permission name.</param>
        internal PermissionRequirement(string permissionName) => PermissionName = permissionName;

        /// <summary>
        /// Gets the permission name.
        /// </summary>
        internal string PermissionName { get; }
    }
}
