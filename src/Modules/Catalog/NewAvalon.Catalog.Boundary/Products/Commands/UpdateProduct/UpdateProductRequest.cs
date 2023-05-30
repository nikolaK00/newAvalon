using System;

namespace NewAvalon.Catalog.Boundary.Products.Commands.UpdateProduct
{
    public sealed record UpdateProductRequest(
        Guid ProductId,
        Guid CreatorId,
        string Name,
        decimal Price,
        decimal Capacity,
        string Description);
}
