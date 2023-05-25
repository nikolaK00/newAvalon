using NewAvalon.Abstractions.Contracts;
using NewAvalon.Abstractions.Messaging;

namespace NewAvalon.Catalog.Boundary.Products.Commands.CreateProduct
{
    public sealed record CreateProductCommand(
        string Name,
        decimal Price,
        decimal Capacity) : ICommand<EntityCreatedResponse>;
}
