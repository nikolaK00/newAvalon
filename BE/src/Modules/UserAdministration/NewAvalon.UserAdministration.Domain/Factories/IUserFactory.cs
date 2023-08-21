using NewAvalon.UserAdministration.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Domain.Factories
{
    public interface IUserFactory
    {
        Task<User> CreateAsync(
            string firstName,
            string lastName,
            string email,
            string username,
            string password,
            DateTime dateOfBirth,
            string address,
            int role,
            CancellationToken cancellationToken);
    }
}
