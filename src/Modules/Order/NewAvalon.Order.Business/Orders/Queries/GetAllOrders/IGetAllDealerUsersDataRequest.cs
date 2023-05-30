﻿using NewAvalon.Abstractions.Data;
using NewAvalon.Boundary.Pagination;
using NewAvalon.Order.Boundary.Orders.Queries.GetAllOrders;

namespace NewAvalon.Order.Business.Orders.Queries.GetAllOrders
{
    public interface IGetAllDealerUserOrdersDataRequest : IDataRequest<(int Page, int ItemsPerPage), PagedList<OrderDetailsResponse>>
    {
    }
}
