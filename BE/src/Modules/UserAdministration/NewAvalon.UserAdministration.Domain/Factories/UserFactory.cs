using Epoche;
using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using NewAvalon.UserAdministration.Domain.Exceptions.Users;
using NewAvalon.UserAdministration.Domain.Repositories;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Domain.Factories
{
    internal sealed class UserFactory : IUserFactory
    {
        private readonly IUserRepository _userRepository;
        private readonly IDealerRepository _dealerRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserAdministrationUnitOfWork _unitOfWork;

        public UserFactory(
            IUserRepository userRepository,
            IDealerRepository dealerRepository,
            IClientRepository clientRepository,
            IRoleRepository roleRepository,
            IUserAdministrationUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _dealerRepository = dealerRepository;
            _clientRepository = clientRepository;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<User> CreateAsync(
            string firstName,
            string lastName,
            string email,
            string username,
            string password,
            DateTime dateOfBirth,
            string address,
            int role,
            CancellationToken cancellationToken)
        {
            await VerifyRequest(email, username, cancellationToken);

            var user = new User(
                new UserId(Guid.NewGuid()),
                firstName,
                lastName,
                username,
                email,
                dateOfBirth,
                address);

            var hashedPassword = GeneratePassword(user.Id.Value, password);

            user.UpdatePassword(hashedPassword);

            _userRepository.Insert(user);

            if (role == Role.Client.Id.Value)
            {
                user.AddRole(_roleRepository.GetByRole(Role.Client));

                user.AddRole(Role.Client);
                var client = new Client(user.Id);
                _clientRepository.Insert(client);
            }
            else if (role == Role.DealerUser.Id.Value)
            {
                user.AddRole(_roleRepository.GetByRole(Role.DealerUser));
                var dealer = new Dealer(user.Id);
                _dealerRepository.Insert(dealer);
            }
            else
            {
                throw new ArgumentException();
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user;
        }

        private string GeneratePassword(Guid userId, string password)
        {
            var keccak = Keccak256.ComputeHash(Encoding.UTF8.GetBytes(userId + password));

            return Encoding.UTF8.GetString(keccak);
        }


        private async Task VerifyRequest(string email, string userName, CancellationToken cancellationToken)
        {
            bool isEmailTaken = await _userRepository.IsEmailTakenAsync(email, cancellationToken);

            if (isEmailTaken)
            {
                throw new UserWithEmailAlreadyExistsException();
            }

            bool isUserNameTaken = await _userRepository.IsUserNameTakenAsync(userName, cancellationToken);

            if (isUserNameTaken)
            {
                throw new UserWithUserNameAlreadyExists();
            }
        }
    }
}
