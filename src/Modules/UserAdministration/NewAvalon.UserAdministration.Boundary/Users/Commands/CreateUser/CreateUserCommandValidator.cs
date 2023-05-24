using FluentValidation;
using NewAvalon.Boundary.Extensions;

namespace NewAvalon.UserAdministration.Boundary.Users.Commands.CreateUser
{
    public sealed class ApproveUserByIdCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public ApproveUserByIdCommandValidator()
        {
            RuleFor(x => x.FirstName).FirstName();

            RuleFor(x => x.LastName).LastName();

            RuleFor(x => x.Email).EmailAddress();
        }
    }
}

