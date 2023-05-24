using Microsoft.EntityFrameworkCore;
using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using NewAvalon.UserAdministration.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Persistence.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly UserAdministrationDbContext _dbContext;

        public UserRepository(UserAdministrationDbContext dbContext) => _dbContext = dbContext;

        public async Task<bool> ExistsAsync(UserId userId, CancellationToken cancellationToken = default) =>
            await _dbContext.Set<User>().AnyAsync(user => user.Id == userId, cancellationToken);

        public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default) =>
            await _dbContext.Set<User>()
                .FirstOrDefaultAsync(user => user.Email == email, cancellationToken);

        public async Task<User> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default) =>
            await _dbContext.Set<User>()
                .FirstOrDefaultAsync(user => user.Id == userId, cancellationToken);

        public async Task<bool> IsEmailTakenAsync(string email, CancellationToken cancellationToken = default) =>
            await _dbContext.Set<User>().AnyAsync(user => user.Email == email, cancellationToken);
        public void Delete(User user) => _dbContext.Set<User>().Remove(user);

        public void Insert(User user) => _dbContext.Set<User>().Add(user);
    }
}
