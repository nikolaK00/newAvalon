using NewAvalon.Domain.Abstractions;
using NewAvalon.Order.Domain.EntityIdentifiers;
using NewAvalon.Order.Domain.Enums;
using System;

namespace NewAvalon.Order.Domain.Entities
{
    public sealed class Order : AggregateRoot<OrderId>, IAuditableEntity
    {
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
    }
}
