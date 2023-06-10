using MediatR;
using NewAvalon.Abstractions.Clock;
using NewAvalon.Abstractions.Contracts;
using NewAvalon.Abstractions.Messaging;
using NewAvalon.Abstractions.Services;
using NewAvalon.UserAdministration.Boundary.Users.Commands.CreateUser;
using NewAvalon.UserAdministration.Boundary.Users.Commands.LoginGoogleUser;
using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Business.Users.Commands.LoginGoogleUser
{
    internal sealed class LoginGoogleUserCommandHandler : ICommandHandler<LoginGoogleUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly ISender _sender;
        private readonly ISystemTime _systemTime;

        public LoginGoogleUserCommandHandler(
            IUserRepository userRepository,
            IJwtProvider jwtProvider,
            ISender sender,
            ISystemTime systemTime)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
            _sender = sender;
            _systemTime = systemTime;
        }

        public async Task<string> Handle(LoginGoogleUserCommand request, CancellationToken cancellationToken)
        {
            var googleUserDetails = _jwtProvider.GetUserDetailsFromGoogleJwt(request.GoogleToken);

            User user = await _userRepository.GetByEmailAsync(googleUserDetails.Email, cancellationToken);

            if (user != null)
            {
                return _jwtProvider.Generate(new GenerateTokenRequest(googleUserDetails.Email, user.Id.Value));

            }

            var createUserCommand = new CreateUserCommand(
                googleUserDetails.Email,
                googleUserDetails.Email,
                null,
                googleUserDetails.FirstName,
                googleUserDetails.LastName,
                _systemTime.UtcNow,
                null,
                request.Roles);

            var response = await _sender.Send(createUserCommand, cancellationToken);

            return _jwtProvider.Generate(new GenerateTokenRequest(googleUserDetails.Email, response.EntityId));
        }
    }
}
