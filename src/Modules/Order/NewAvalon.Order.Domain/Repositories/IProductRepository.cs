using NewAvalon.Order.Domain.EntityIdentifiers;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Order.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Entities.Product> GetByIdAsync(ProductId productId, CancellationToken cancellationToken = default);
    }
}
