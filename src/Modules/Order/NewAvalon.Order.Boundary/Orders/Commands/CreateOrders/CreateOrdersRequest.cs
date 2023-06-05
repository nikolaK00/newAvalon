using System;
using System.Collections.Generic;

namespace NewAvalon.Order.Boundary.Orders.Commands.CreateOrders
{
    public sealed record CreateOrdersRequest(List<AddProductRequest> Products);

    public sealed record AddProductRequest(Guid Id, decimal Quantity);
}
