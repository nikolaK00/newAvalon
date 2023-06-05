using MediatR;
using NewAvalon.Abstractions.Messaging;
using NewAvalon.Catalog.Boundary.Products.Commands.DeleteProduct;
using NewAvalon.Catalog.Domain.EntityIdentifiers;
using NewAvalon.Catalog.Domain.Repositories;
using NewAvalon.Domain.Exceptions.Products;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Catalog.Business.Products.Commands.DeleteProduct
{
    internal sealed class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICatalogUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(
            IProductRepository productRepository,
            ICatalogUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(new ProductId(request.ProductId), cancellationToken);

            if (product is null || product.CreatorId != request.CreatorId)
            {
                throw new ProductNotFoundException(request.ProductId);
            }

            _productRepository.Delete(product);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
