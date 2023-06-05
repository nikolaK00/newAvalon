using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewAvalon.Order.Domain.Entities;
using NewAvalon.Order.Domain.EntityIdentifiers;
using NewAvalon.Order.Persistence.Constants;

namespace NewAvalon.Order.Persistence.Configurations
{
    internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            ConfigureDataStructure(builder);
            ConfigureRelationships(builder);
        }

        private static void ConfigureDataStructure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(TableNames.Products);

            builder.HasKey(product => product.Id);

            builder.Property(product => product.Id).HasConversion(productId => productId.Value, value => new ProductId(value));

            builder.Property(order => order.CreatedOnUtc).IsRequired();

            builder.Property(order => order.ModifiedOnUtc);

            builder.Property(order => order.CatalogProductId).IsRequired();

            builder.Property(order => order.Quantity).IsRequired();
        }

        private static void ConfigureRelationships(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne<Domain.Entities.Order>()
                .WithMany(order => order.Products)
                .HasForeignKey(lineItem => lineItem.OrderId);
        }
    }
}
