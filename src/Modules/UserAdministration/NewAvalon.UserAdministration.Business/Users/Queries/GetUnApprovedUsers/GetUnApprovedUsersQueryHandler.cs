using NewAvalon.Abstractions.Messaging;
using NewAvalon.Boundary.Pagination;
using NewAvalon.UserAdministration.Boundary.Users.Queries.GetUnApprovedUsers;
using NewAvalon.UserAdministration.Boundary.Users.Queries.GetUser;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Business.Users.Queries.GetUnApprovedUsers
{
    internal sealed class GetUnApprovedUsersQueryHandler : IQueryHandler<GetUnApprovedUsersQuery, PagedList<UserDetailsResponse>>
    {
        private readonly IGetUnApprovedUsersDataRequest _getUnApprovedUsersDataRequest;

        public GetUnApprovedUsersQueryHandler(IGetUnApprovedUsersDataRequest getUnApprovedUsersDataRequest)
        {
            _getUnApprovedUsersDataRequest = getUnApprovedUsersDataRequest;
        }

        public async Task<PagedList<UserDetailsResponse>> Handle(GetUnApprovedUsersQuery request, CancellationToken cancellationToken) =>
            await _getUnApprovedUsersDataRequest.GetAsync((request.Page, request.ItemsPerPage), cancellationToken);
    }
}
