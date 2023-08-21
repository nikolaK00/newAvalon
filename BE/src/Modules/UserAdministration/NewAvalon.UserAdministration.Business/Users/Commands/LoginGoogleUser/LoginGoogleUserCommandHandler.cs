using NewAvalon.Abstractions.Clock;
using NewAvalon.Abstractions.Contracts;
using NewAvalon.Abstractions.Messaging;
using NewAvalon.Abstractions.Services;
using NewAvalon.UserAdministration.Boundary.Users.Commands.LoginGoogleUser;
using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.Factories;
using NewAvalon.UserAdministration.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Business.Users.Commands.LoginGoogleUser
{
    internal sealed class LoginGoogleUserCommandHandler : ICommandHandler<LoginGoogleUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IUserFactory _userFactory;
        private readonly ISystemTime _systemTime;

        public LoginGoogleUserCommandHandler(
            IUserRepository userRepository,
            IJwtProvider jwtProvider,
            IUserFactory userFactory,
            ISystemTime systemTime)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
            _userFactory = userFactory;
            _systemTime = systemTime;
        }

        public async Task<string> Handle(LoginGoogleUserCommand request, CancellationToken cancellationToken)
        {
            var googleUserDetails = _jwtProvider.GetUserDetailsFromGoogleJwt(request.Token);

            User user = await _userRepository.GetByEmailAsync(googleUserDetails.Email, cancellationToken);

            if (user != null)
            {
                return _jwtProvider.Generate(new GenerateTokenRequest(googleUserDetails.Email, user.Id.Value));
            }

            var newUser = await _userFactory.CreateAsync(
                googleUserDetails.FirstName,
                googleUserDetails.LastName,
                googleUserDetails.Email,
                googleUserDetails.Email,
                null,
                _systemTime.UtcNow,
                null,
                request.Role,
                cancellationToken);

            return _jwtProvider.Generate(new GenerateTokenRequest(googleUserDetails.Email, newUser.Id.Value));
        }
    }
}
