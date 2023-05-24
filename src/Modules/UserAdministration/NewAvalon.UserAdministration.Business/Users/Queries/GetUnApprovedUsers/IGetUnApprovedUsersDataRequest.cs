using NewAvalon.Abstractions.Data;
using NewAvalon.Boundary.Pagination;
using NewAvalon.UserAdministration.Boundary.Users.Queries.GetUser;

namespace NewAvalon.UserAdministration.Business.Users.Queries.GetUnApprovedUsers
{
    public interface IGetUnApprovedUsersDataRequest : IDataRequest<(int Page, int ItemsPerPage), PagedList<UserDetailsResponse>>
    {
    }
}
