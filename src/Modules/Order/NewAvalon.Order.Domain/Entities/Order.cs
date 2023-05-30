using NewAvalon.Domain.Abstractions;
using NewAvalon.Order.Domain.EntityIdentifiers;
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

        public DateTime CreatedOnUtc { get; }

        public DateTime? ModifiedOnUtc { get; }
    }
}
