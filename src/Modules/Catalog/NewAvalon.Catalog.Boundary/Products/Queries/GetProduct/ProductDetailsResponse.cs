using System;

namespace NewAvalon.Catalog.Boundary.Products.Queries.GetProduct
{
    public sealed record ProductDetailsResponse(Guid Id, string Name, decimal Price, decimal Capacity);
}
