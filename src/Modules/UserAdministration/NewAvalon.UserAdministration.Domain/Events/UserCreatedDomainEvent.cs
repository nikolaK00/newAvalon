using NewAvalon.Domain.Abstractions;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;

namespace NewAvalon.UserAdministration.Domain.Events
{
    public sealed record UserCreatedDomainEvent(UserId UserId) : IDomainEvent;
}
