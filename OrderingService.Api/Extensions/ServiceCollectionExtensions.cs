using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderingService.Api.Application.Queries;
using OrderingService.Api.Application.Queries.Order;
using OrderingService.Api.Application.Settings;
using OrderingService.Infrastructure;

namespace OrderingService.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Регистрация зависимостей приложения
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();

            services.AddSingleton(e => appSettings);

            services.AddDbContext<OrdersContext>(options =>
                options.UseSqlServer(appSettings.DbSettings.ConnectionString));

            services.AddTransient<IDbConnectionFactory, DbConnectionFactory>(e =>
            {
                var settings = e.GetRequiredService<AppSettings>();
                return new DbConnectionFactory(settings.DbSettings.ConnectionString);
            });

            services.AddTransient<IOrderQueries, OrderQueries>();
        }
    }
}
