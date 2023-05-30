using NewAvalon.Domain.Errors;
using NewAvalon.Domain.Exceptions;
using System;

namespace NewAvalon.Catalog.Domain.Exceptions.Products
{
    public sealed class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid id)
            : base("Product not found", $"The product with specified identifier {id} was not found.", Error.ProductNotFound)
        {
        }
    }
}
