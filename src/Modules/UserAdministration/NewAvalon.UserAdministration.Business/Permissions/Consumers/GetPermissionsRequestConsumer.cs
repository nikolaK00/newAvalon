using MassTransit;
using NewAvalon.Messaging.Contracts.Permissions;
using NewAvalon.UserAdministration.Business.Contracts.Permissions;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Business.Permissions.Consumers
{
    public sealed class GetPermissionsRequestConsumer : IConsumer<IGetPermissionsRequest>
    {
        private readonly IGetPermissionsDataRequest _getPermissionsDataRequest;

        public GetPermissionsRequestConsumer(IGetPermissionsDataRequest getPermissionsDataRequest) => _getPermissionsDataRequest = getPermissionsDataRequest;

        public async Task Consume(ConsumeContext<IGetPermissionsRequest> context)
        {
            var request = new GetPermissionsRequest(context.Message.UserIdentityProviderId);

            string[] permissionNames = await _getPermissionsDataRequest.GetAsync(request, context.CancellationToken);

            var response = new GetPermissionsResponse
            {
                PermissionNames = permissionNames
            };

            await context.RespondAsync<IGetPermissionsResponse>(response);
        }
    }
}
