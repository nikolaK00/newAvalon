﻿using System;
using System.Collections.Generic;

namespace NewAvalon.Order.Boundary.Orders.Queries.GetAllOrders
{
    public sealed record OrderDetailsResponse(
        Guid Id,
        Guid OwnerId,
        Guid DealerId,
        string Comment,
        string DeliveryAddress,
        int Status,
        List<ProductDetailsResponse> ProductDetailsResponses,
        decimal DeliveryPrice,
        decimal FullPrice);

    public sealed record ProductDetailsResponse(Guid Id, Guid OrderId, Guid CatalogProductId, decimal Quantity, decimal Price, decimal FullPrice);

}
