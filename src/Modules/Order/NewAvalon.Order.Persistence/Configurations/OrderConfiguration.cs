using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewAvalon.Order.Domain.EntityIdentifiers;
using NewAvalon.Order.Persistence.Constants;

namespace NewAvalon.Order.Persistence.Configurations
{
    internal sealed class OrderConfiguration : IEntityTypeConfiguration<Domain.Entities.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Order> builder)
        {
            ConfigureDataStructure(builder);

            ConfigureRelationships(builder);
        }

        private static void ConfigureDataStructure(EntityTypeBuilder<Domain.Entities.Order> builder)
        {
            builder.ToTable(TableNames.Orders);

            builder.HasKey(order => order.Id);

            builder.Property(order => order.Id)
                .ValueGeneratedNever()
                .HasConversion(orderId => orderId.Value, value => new OrderId(value));

            builder.Property(dealer => dealer.CreatedOnUtc).IsRequired();

            builder.Property(dealer => dealer.ModifiedOnUtc);
        }

        private static void ConfigureRelationships(EntityTypeBuilder<Domain.Entities.Order> builder)
        {
        }
    }
}
