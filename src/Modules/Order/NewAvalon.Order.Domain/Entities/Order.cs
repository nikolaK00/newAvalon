using NewAvalon.Domain.Abstractions;
using NewAvalon.Order.Domain.EntityIdentifiers;
using NewAvalon.Order.Domain.Enums;
using NewAvalon.Order.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewAvalon.Order.Domain.Entities
{
    public sealed class Order : AggregateRoot<OrderId>, IAuditableEntity
    {
        private readonly List<Product> _products = new();

        public Order(OrderId id, Guid ownerId, Guid dealerId) : base(id)
        {
            OwnerId = ownerId;
            DealerId = dealerId;
            Status = OrderStatus.Shipping;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Order"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        private Order()
        {
        }

        public DateTime CreatedOnUtc { get; private set; }

        public DateTime? ModifiedOnUtc { get; private set; }

        public OrderStatus Status { get; private set; }

        public Guid OwnerId { get; private set; }

        public Guid DealerId { get; private set; }

        public IReadOnlyCollection<Product> Products => _products.ToList();

        public Product AddProduct(Guid catalogProductId, decimal quantity)
        {
            var productId = new ProductId(Guid.NewGuid());

            var product = new Product(productId, Id, catalogProductId, quantity);

            _products.Add(product);

            RaiseDomainEvent(new ProductAddedDomainEvent(productId));

            return product;
        }
    }
}
