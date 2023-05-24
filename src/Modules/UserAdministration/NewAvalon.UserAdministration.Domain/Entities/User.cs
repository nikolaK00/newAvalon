using NewAvalon.Domain.Abstractions;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using NewAvalon.UserAdministration.Domain.Events;
using System;

namespace NewAvalon.UserAdministration.Domain.Entities
{
    public sealed class User : AggregateRoot<UserId>, IAuditableEntity
    {
        public User(UserId id, string firstName, string lastName, string email)
            : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;

            RaiseDomainEvent(new UserCreatedDomainEvent(Id));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        private User()
        {
        }

        public Guid? IdentityProviderId { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public DateTime CreatedOnUtc { get; private set; }

        public DateTime? ModifiedOnUtc { get; private set; }
    }
}
