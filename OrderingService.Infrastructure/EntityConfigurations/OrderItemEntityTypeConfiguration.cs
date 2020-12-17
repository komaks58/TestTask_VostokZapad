using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderingService.Domain.AggregatesModel.CustomerAggregate;
using OrderingService.Domain.AggregatesModel.OrderAggregate;

namespace OrderingService.Infrastructure.EntityConfigurations
{
    public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("orderItems");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.ProductsQuantity).IsRequired();

            builder
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(e => e.ProductId)
                .IsRequired();

            builder
                .HasOne<Order>()
                .WithMany()
                .HasForeignKey(e => e.OrderId)
                .IsRequired();
        }
    }
}