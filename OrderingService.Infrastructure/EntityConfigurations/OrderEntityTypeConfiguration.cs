using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderingService.Domain.AggregatesModel.CustomerAggregate;
using OrderingService.Domain.AggregatesModel.OrderAggregate;

namespace OrderingService.Infrastructure.EntityConfigurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders");

            builder.HasKey(e => e.Id);

            builder.HasMany<OrderItem>(e => e.OrderItems);

            builder
                .HasOne<Customer>()
                .WithMany()
                .HasForeignKey(e => e.CustomerId)
                .IsRequired();
        }
    }
}