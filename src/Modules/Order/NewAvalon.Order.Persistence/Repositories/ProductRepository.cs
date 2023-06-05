using Microsoft.EntityFrameworkCore;
using NewAvalon.Order.Domain.Entities;
using NewAvalon.Order.Domain.EntityIdentifiers;
using NewAvalon.Order.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Order.Persistence.Repositories
{
    internal sealed class ProductRepository : IProductRepository
    {
        private readonly OrderDbContext _dbContext;

        public ProductRepository(OrderDbContext dbContext) => _dbContext = dbContext;

        public async Task<Product> GetByIdAsync(ProductId productId, CancellationToken cancellationToken = default) =>
            await _dbContext.Set<Product>().FirstOrDefaultAsync(product => product.Id == productId, cancellationToken);
    }
}