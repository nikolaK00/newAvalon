using NewAvalon.Abstractions.Messaging;
using NewAvalon.Messaging.Contracts;
using NewAvalon.UserAdministration.Boundary.Users.Queries.GetUser;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Business.Users.Queries.GetUser
{
    internal sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDetailsResponse>
    {
        private readonly ITestDataRequestIRequestClientDataRequest _testDataRequestIRequestClient;

        public GetUserByIdQueryHandler(ITestDataRequestIRequestClientDataRequest testDataRequestIRequestClient)
        {
            _testDataRequestIRequestClient = testDataRequestIRequestClient;
        }

        public async Task<UserDetailsResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken) =>
            await _testDataRequestIRequestClient.GetAsync(new UserId(request.UserId), cancellationToken);

        public sealed class ExampleRequest : IExampleRequest
        {
            public bool IsWorking { get; set; }
        }
    }
}
