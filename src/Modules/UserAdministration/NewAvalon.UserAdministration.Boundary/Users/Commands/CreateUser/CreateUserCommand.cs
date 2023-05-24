using NewAvalon.Abstractions.Contracts;
using NewAvalon.Abstractions.Messaging;

namespace NewAvalon.UserAdministration.Boundary.Users.Commands.CreateUser
{
    public sealed record CreateUserCommand(
        string FirstName,
        string LastName,
        string Email) : ICommand<EntityCreatedResponse>;
}
