using System;
using System.Collections.Generic;

namespace NewAvalon.UserAdministration.Boundary.Users.Queries.GetUser
{
    public sealed record UserDetailsResponse(Guid Id, string FirstName, string LastName, string Email, List<string> Roles);
}
