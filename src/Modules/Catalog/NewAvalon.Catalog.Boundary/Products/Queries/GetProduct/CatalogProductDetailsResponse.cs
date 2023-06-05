using System;

namespace NewAvalon.Catalog.Boundary.Products.Queries.GetProduct
{
    public sealed record CatalogProductDetailsResponse(Guid Id, string Name, decimal Price, decimal Capacity, string Description);
}
