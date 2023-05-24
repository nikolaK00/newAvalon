using NewAvalon.Catalog.Domain.EntityIdentifiers;
using NewAvalon.Catalog.Domain.ValueObjects;
using NewAvalon.Domain.Abstractions;
using System;

namespace NewAvalon.Catalog.Domain.Entities
{
    public sealed class Product : AggregateRoot<ProductId>, IAuditableEntity
    {
        public Product(ProductId id, string name, decimal price, decimal capacity, string description)
            : base(id)
        {
            Name = name;
            Price = price;
            Capacity = capacity;
            Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        private Product()
        {
        }

        public string Name { get; private set; }

        public decimal Price { get; private set; }

        public decimal Capacity { get; private set; }

        public string Description { get; private set; }

        public ProductImage ProductImage { get; private set; }

        public DateTime CreatedOnUtc { get; private set; }

        public DateTime? ModifiedOnUtc { get; private set; }

        public void ChangeProductImage(ProductImage productImage)
        {
            if (ProductImage == productImage)
            {
                return;
            }

            ProductImage ??= ProductImage.Empty;

            ProductImage = productImage;
        }

        public void RemoveProductImage() => ProductImage = ProductImage.Empty;
    }
}
