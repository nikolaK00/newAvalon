using System;

namespace NewAvalon.Messaging.Contracts.Permissions
{
    public interface IGetPermissionsRequest
    {
        Guid UserIdentityProviderId { get; set; }
    }
}
