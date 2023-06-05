using NewAvalon.Domain.Abstractions;
using NewAvalon.Order.Domain.EntityIdentifiers;
using System;

namespace NewAvalon.Order.Domain.Entities
{
    public sealed class Product : AggregateRoot<ProductId>, IAuditableEntity
    {
        internal Product(
            ProductId id,
            OrderId orderId,
            Guid catalogProductId,
            decimal quantity) : base(id)
        {
            OrderId = orderId;
            CatalogProductId = catalogProductId;
            Quantity = quantity;
        }

        private Product(ProductId id) : base(id)
        {
        }

        private Product()
        {
        }

        public OrderId OrderId { get; private set; }

        public Guid CatalogProductId { get; private set; }

        public decimal Quantity { get; private set; }

        public DateTime CreatedOnUtc { get; private set; }

        public DateTime? ModifiedOnUtc { get; private set; }
    }
}
