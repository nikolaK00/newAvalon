using System;

namespace NewAvalon.UserAdministration.Business.Permissions.Consumers
{
    public sealed record GetPermissionsRequest(Guid UserIdentityProviderId);
}
