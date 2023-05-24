using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Notification.Domain.Repositories
{
    public interface INotificationUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
