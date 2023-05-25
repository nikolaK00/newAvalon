using NewAvalon.Abstractions.Messaging;
using NewAvalon.Catalog.Boundary.Products.Queries.GetProduct;
using NewAvalon.Catalog.Domain.EntityIdentifiers;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Catalog.Business.Products.Queries.GetProduct
{
    internal sealed class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductDetailsResponse>
    {
        private readonly IGetProductByIdDataRequest _getProductByIdDataRequest;

        public GetProductByIdQueryHandler(IGetProductByIdDataRequest getProductByIdDataRequest)
        {
            _getProductByIdDataRequest = getProductByIdDataRequest;
        }

        public async Task<ProductDetailsResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken) =>
            await _getProductByIdDataRequest.GetAsync(new ProductId(request.UserId), cancellationToken);
    }
}
