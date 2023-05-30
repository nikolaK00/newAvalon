using Mapster;
using MassTransit;
using NewAvalon.Messaging.Contracts.Users;
using NewAvalon.UserAdministration.Business.Contracts.Users;
using NewAvalon.UserAdministration.Business.Users.Queries.GetUser;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Business.Users.Consumers
{
    public sealed class UserDetailsRequestConsumer : IConsumer<IUserDetailsRequest>
    {
        private readonly IGetUserByIdDataRequest _getUserByIdDataRequest;

        public UserDetailsRequestConsumer(IGetUserByIdDataRequest getUserByIdDataRequest) =>
            _getUserByIdDataRequest = getUserByIdDataRequest;

        public async Task Consume(ConsumeContext<IUserDetailsRequest> context)
        {
            var userDetails =
                await _getUserByIdDataRequest.GetAsync(new UserId(context.Message.Id), context.CancellationToken);

            var response = userDetails.Adapt<UserDetailsResponse>();

            await context.RespondAsync<IUserDetailsResponse>(response.Adapt<IUserDetailsResponse>());
        }
    }
}
