using NewAvalon.Abstractions.Messaging;
using NewAvalon.Boundary.Pagination;
using NewAvalon.Catalog.Boundary.Products.Queries.GetProduct;

namespace NewAvalon.Catalog.Boundary.Products.Queries.GetProducts
{
    public sealed record GetProductsQuery(int Page, int ItemsPerPage) : IQuery<PagedList<ProductDetailsResponse>>;
}
