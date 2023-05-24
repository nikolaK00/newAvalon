using NewAvalon.Abstractions.Contracts;
using NewAvalon.Abstractions.Messaging;
using NewAvalon.UserAdministration.Boundary.Users.Commands.CreateUser;
using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.Exceptions.Users;
using NewAvalon.UserAdministration.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Business.Users.Commands.CreateUser
{
    //Should use factory, only for testing like this
    internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, EntityCreatedResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserAdministrationUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(
            IUserRepository userRepository,
            IUserAdministrationUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<EntityCreatedResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            bool isEmailTaken = await _userRepository.IsEmailTakenAsync(request.Email, cancellationToken);

            if (isEmailTaken)
            {
                throw new UserWithEmailAlreadyExistsException();
            }

            var user = new User(
                new Domain.EntityIdentifiers.UserId(Guid.NewGuid()),
                request.FirstName,
                request.LastName,
                request.Email);

            _userRepository.Insert(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new EntityCreatedResponse(user.Id.Value);
        }
    }
}
