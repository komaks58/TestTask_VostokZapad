using Microsoft.EntityFrameworkCore;
using OrderingService.Domain.AggregatesModel.CustomerAggregate;
using OrderingService.Domain.AggregatesModel.OrderAggregate;
using OrderingService.Infrastructure.EntityConfigurations;

namespace OrderingService.Infrastructure
{
    public class OrdersContext : DbContext
    {
        public OrdersContext(DbContextOptions<OrdersContext> options) : base(options)
        {
        }

        /// <summary>
        /// Пользователи
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// Заказы
        /// </summary>
        public DbSet<Order> Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
        }
    }
}
