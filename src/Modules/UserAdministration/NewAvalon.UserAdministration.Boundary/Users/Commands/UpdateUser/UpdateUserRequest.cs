namespace NewAvalon.UserAdministration.Boundary.Users.Commands.UpdateUser
{
    public sealed record UpdateUserRequest(
        string FirstName,
        string LastName,
        string Email);
}
