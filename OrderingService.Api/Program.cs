using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using OrderingService.Api.Extensions;
using OrderingService.Infrastructure;

namespace OrderingService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .Build()
                .MigrateDbContext<OrdersContext>()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
