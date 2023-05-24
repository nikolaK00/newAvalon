namespace NewAvalon.UserAdministration.Boundary.Users.Commands.CreateUser
{
    public sealed record CreateUserRequest(
        string FirstName,
        string LastName,
        string Email);
}
