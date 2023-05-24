using NewAvalon.Domain.Abstractions;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using NewAvalon.UserAdministration.Domain.Events;
using NewAvalon.UserAdministration.Domain.ValueObjects;
using System;

namespace NewAvalon.UserAdministration.Domain.Entities
{
    public sealed class User : AggregateRoot<UserId>, IAuditableEntity
    {
        public User(UserId id, string firstName, string lastName, string userName, string email, string password, DateTime dateOfBirth, string address)
            : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;
            Password = password;
            DateOfBirth = dateOfBirth;
            Address = address;
            Approved = false;

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

        public string UserName { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public DateTime DateOfBirth { get; private set; }

        public string Address { get; private set; }

        public DateTime CreatedOnUtc { get; private set; }

        public DateTime? ModifiedOnUtc { get; private set; }

        public ProfileImage ProfileImage { get; private set; }

        public bool Approved { get; private set; }
    }
}
