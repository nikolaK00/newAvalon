using NewAvalon.Domain.Abstractions;

namespace NewAvalon.Catalog.Domain.EntityIdentifiers
{
    public sealed record ProductId(int Value) : IEntityId;
}
