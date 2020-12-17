using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace OrderingService.Api.Extensions
{
    /// <summary>
    /// Расширения связанные с базой данных
    /// </summary>
    public static class DbExtensions
    {
        /// <summary>
        /// Применение миграций к базе данных
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="webHost"></param>
        /// <returns></returns>
        public static IWebHost MigrateDbContext<TContext>(this IWebHost webHost) where TContext : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                scope.ServiceProvider
                    .GetService<TContext>()
                    .Database
                    .Migrate();
            }

            return webHost;
        }
    }
}
