using NewAvalon.Abstractions.Data;
using NewAvalon.Boundary.Pagination;
using NewAvalon.Catalog.Boundary.Products.Queries.GetProduct;

namespace NewAvalon.Catalog.Business.Products.Queries.GetProducts
{
    public interface IGetAllProductsDataRequest : IDataRequest<(int Page, int ItemsPerPage), PagedList<ProductDetailsResponse>>
    {
    }
}
