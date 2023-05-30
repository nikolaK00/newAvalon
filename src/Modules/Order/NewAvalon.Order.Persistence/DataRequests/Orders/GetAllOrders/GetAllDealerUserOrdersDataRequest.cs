using Microsoft.EntityFrameworkCore;
using NewAvalon.Boundary.Pagination;
using NewAvalon.Order.Boundary.Orders.Queries.GetAllOrders;
using NewAvalon.Order.Business.Orders.Queries.GetAllOrders;
using NewAvalon.Order.Domain.Enums;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Order.Persistence.DataRequests.Orders.GetAllOrders
{
    internal sealed class GetAllDealerUserOrdersDataRequest : IGetAllDealerUserOrdersDataRequest
    {
        private readonly OrderDbContext _dbContext;

        public GetAllDealerUserOrdersDataRequest(OrderDbContext dbContext) => _dbContext = dbContext;

        public async Task<PagedList<OrderDetailsResponse>> GetAsync((int Page, int ItemsPerPage) request, CancellationToken cancellationToken = default)
        {
            var orders = await _dbContext.Set<Domain.Entities.Order>()
                .Where(order => order.Status != OrderStatus.Shipping)
                .OrderBy(order => order.CreatedOnUtc)
                .Skip((request.Page - 1) * request.ItemsPerPage)
                .Take(request.ItemsPerPage)
                .ToListAsync(cancellationToken);

            var response = orders.Select(order => new OrderDetailsResponse(order.Id.Value));

            var count = await _dbContext.Set<Domain.Entities.Order>().CountAsync(cancellationToken: cancellationToken);

            return new PagedList<OrderDetailsResponse>(response, count, request.Page, request.ItemsPerPage);
        }
    }
}
