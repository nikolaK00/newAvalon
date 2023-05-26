using NewAvalon.UserAdministration.Boundary.Contracts.Users;
using System;
using System.Collections.Generic;

namespace NewAvalon.UserAdministration.Boundary.Users.Queries.GetLoggedUser
{
    public sealed record UserWithPermissionsResponse(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        List<RoleResponse> Roles,
        List<PermissionResponse> Permissions);
}
