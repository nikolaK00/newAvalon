using Microsoft.EntityFrameworkCore;
using NewAvalon.Order.Domain.EntityIdentifiers;
using NewAvalon.Order.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Order.Persistence.Repositories
{
    internal sealed class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _dbContext;

        public OrderRepository(OrderDbContext dbContext) => _dbContext = dbContext;

        public void Insert(Domain.Entities.Order order) => _dbContext.Set<Domain.Entities.Order>().Add(order);

        public async Task<Domain.Entities.Order> GetByIdAsync(OrderId orderId, CancellationToken cancellationToken = default) =>
            await _dbContext.Set<Domain.Entities.Order>()
                .FirstOrDefaultAsync(dealer => dealer.Id == orderId, cancellationToken);
    }
}