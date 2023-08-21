using NewAvalon.Abstractions.Contracts;
using NewAvalon.Abstractions.Messaging;
using NewAvalon.UserAdministration.Boundary.Users.Commands.CreateUser;
using NewAvalon.UserAdministration.Domain.Factories;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Business.Users.Commands.CreateUser
{
    internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, EntityCreatedResponse>
    {
        private readonly IUserFactory _userFactory;

        public CreateUserCommandHandler(IUserFactory userFactory) => _userFactory = userFactory;

        public async Task<EntityCreatedResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userFactory.CreateAsync(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Username,
                request.Password,
                request.DateOfBirth,
                request.Address,
                request.Roles,
                cancellationToken);

            return new EntityCreatedResponse(user.Id.Value);
        }
    }
}
