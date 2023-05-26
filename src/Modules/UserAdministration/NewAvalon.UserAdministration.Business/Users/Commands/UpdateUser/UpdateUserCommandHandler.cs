using MediatR;
using NewAvalon.Abstractions.Messaging;
using NewAvalon.UserAdministration.Boundary.Users.Commands.UpdateUser;
using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.Exceptions.Users;
using NewAvalon.UserAdministration.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Business.Users.Commands.UpdateUser
{
    internal sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserAdministrationUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IUserRepository userRepository, IUserAdministrationUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (user is null)
            {
                throw new UserNotFoundByEmailException(request.Email);
            }

            user.Update(request.FirstName, request.LastName, string.Empty, string.Empty);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
