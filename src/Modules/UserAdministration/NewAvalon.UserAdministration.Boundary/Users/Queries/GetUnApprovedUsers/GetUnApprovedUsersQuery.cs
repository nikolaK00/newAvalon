using NewAvalon.Abstractions.Messaging;
using NewAvalon.Boundary.Pagination;
using NewAvalon.UserAdministration.Boundary.Users.Queries.GetUser;

namespace NewAvalon.UserAdministration.Boundary.Users.Queries.GetUnApprovedUsers
{
    public sealed record GetUnApprovedUsersQuery(int Page, int ItemsPerPage) : IQuery<PagedList<UserDetailsResponse>>;
}
