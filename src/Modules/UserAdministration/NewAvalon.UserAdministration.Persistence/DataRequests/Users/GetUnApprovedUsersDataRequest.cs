using Microsoft.EntityFrameworkCore;
using NewAvalon.Boundary.Pagination;
using NewAvalon.UserAdministration.Boundary.Users.Queries.GetUser;
using NewAvalon.UserAdministration.Business.Users.Queries.GetUnApprovedUsers;
using NewAvalon.UserAdministration.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Persistence.DataRequests.Users
{
    internal sealed class GetUnApprovedUsersDataRequest : IGetUnApprovedUsersDataRequest
    {
        private readonly UserAdministrationDbContext _dbContext;

        public GetUnApprovedUsersDataRequest(UserAdministrationDbContext dbContext) => _dbContext = dbContext;

        public async Task<PagedList<UserDetailsResponse>> GetAsync((int Page, int ItemsPerPage) request, CancellationToken cancellationToken = default)
        {
            var users = await _dbContext.Set<User>()
                .AsNoTracking()
                .Where(user => !user.Approved)
                .OrderBy(user => user.CreatedOnUtc)
                .Skip((request.Page - 1) * request.ItemsPerPage)
                .Take(request.ItemsPerPage)
                .ToListAsync(cancellationToken);

            IEnumerable<UserDetailsResponse> userDetailsResponses = users
                .Select(user => new UserDetailsResponse(
                    user.Id.Value,
                    user.FirstName,
                    user.LastName,
                    user.Email));

            var count = _dbContext.Set<User>().Count();

            return new PagedList<UserDetailsResponse>(userDetailsResponses, count, request.Page, request.ItemsPerPage);
        }
    }
}
