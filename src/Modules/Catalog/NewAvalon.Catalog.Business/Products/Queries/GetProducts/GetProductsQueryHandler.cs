using NewAvalon.Abstractions.Messaging;
using NewAvalon.Boundary.Pagination;
using NewAvalon.Catalog.Boundary.Products.Queries.GetProduct;
using NewAvalon.Catalog.Boundary.Products.Queries.GetProducts;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Catalog.Business.Products.Queries.GetProducts
{
    internal sealed class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, PagedList<ProductDetailsResponse>>
    {
        private readonly IGetAllProductsDataRequest _getAllProductsDataRequest;

        public GetProductsQueryHandler(IGetAllProductsDataRequest getAllProductsDataRequest)
        {
            _getAllProductsDataRequest = getAllProductsDataRequest;
        }

        public async Task<PagedList<ProductDetailsResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken) =>
            await _getAllProductsDataRequest.GetAsync((request.Page, request.ItemsPerPage), cancellationToken);
    }
}
