using NewAvalon.Order.Domain.EntityIdentifiers;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Order.Domain.Repositories
{
    public interface IOrderRepository
    {
        void Insert(Entities.Order order);

        Task<Entities.Order> GetByIdAsync(OrderId orderId, CancellationToken cancellationToken = default);
    }
}
