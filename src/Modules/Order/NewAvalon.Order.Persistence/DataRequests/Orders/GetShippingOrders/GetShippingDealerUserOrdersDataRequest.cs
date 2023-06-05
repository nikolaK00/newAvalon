﻿using Microsoft.EntityFrameworkCore;
using NewAvalon.Boundary.Pagination;
using NewAvalon.Order.Boundary.Orders.Queries.GetAllOrders;
using NewAvalon.Order.Business.Orders.Queries.GetShippingOrders;
using NewAvalon.Order.Domain.Enums;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Order.Persistence.DataRequests.Orders.GetShippingOrders
{
    internal sealed class GetShippingDealerUserOrdersDataRequest : IGetShippingDealerUserOrdersDataRequest
    {
        private readonly OrderDbContext _dbContext;

        public GetShippingDealerUserOrdersDataRequest(OrderDbContext dbContext) => _dbContext = dbContext;

        public async Task<PagedList<OrderDetailsResponse>> GetAsync((Guid DealerId, int Page, int ItemsPerPage) request, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Set<Domain.Entities.Order>()
                .Where(order => order.Status == OrderStatus.Shipping && order.DealerId == request.DealerId);

            var orders = await query
                .OrderBy(order => order.CreatedOnUtc)
                .Skip((request.Page - 1) * request.ItemsPerPage)
                .Take(request.ItemsPerPage)
                .ToListAsync(cancellationToken);

            var response = orders.Select(order => new OrderDetailsResponse(order.Id.Value));

            var count = await query.CountAsync(cancellationToken);

            return new PagedList<OrderDetailsResponse>(response, count, request.Page, request.ItemsPerPage);
        }
    }
}