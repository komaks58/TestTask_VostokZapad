using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderingService.Domain.AggregatesModel.OrderAggregate;

namespace OrderingService.Infrastructure.EntityConfigurations
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(e => e.Price).IsRequired();

            builder.Property(e => e.Description)
                .HasMaxLength(500)
                .IsRequired(false);
        }
    }
}