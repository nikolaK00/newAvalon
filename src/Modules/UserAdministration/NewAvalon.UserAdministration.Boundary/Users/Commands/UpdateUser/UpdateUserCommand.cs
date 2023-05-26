using MediatR;
using NewAvalon.Abstractions.Messaging;

namespace NewAvalon.UserAdministration.Boundary.Users.Commands.UpdateUser
{
    public sealed record UpdateUserCommand(
        string FirstName,
        string LastName,
        string Email) : ICommand<Unit>;
}
