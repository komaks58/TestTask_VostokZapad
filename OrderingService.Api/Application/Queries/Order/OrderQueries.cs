using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using OrderingService.Api.Application.Queries.Order.Models;

namespace OrderingService.Api.Application.Queries.Order
{
    public class OrderQueries : IOrderQueries
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        private const string _baseOrdersSqlQuery = @"
                        SELECT o.Id, o.CustomerId, o.CreationDateTimeUtc, oi.ProductsQuantity as Quantity, p.Name, p.Price
                        FROM orders o
                        LEFT JOIN orderItems oi ON o.Id = oi.OrderId 
                        LEFT JOIN products p ON oi.ProductId = p.Id";


        public OrderQueries(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory ?? throw new ArgumentNullException(nameof(dbConnectionFactory));
        }

        /// <summary>
        /// Получение заказа по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<OrderDto> GetOrderAsync(long id)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                OrderDto result = null;

                connection.Open();

                var sqlQueryBuilder = new StringBuilder(_baseOrdersSqlQuery);
                sqlQueryBuilder.Append(@" WHERE ");

                var filters = new List<string>() { "o.Id = @id" };

                AddFilters(sqlQueryBuilder, filters);

                await connection.QueryAsync<OrderDto, OrderItemDto, OrderDto>(sqlQueryBuilder.ToString(),
                    (orderDto, orderItemDto) =>
                    {
                        if (result == null)
                            result = orderDto;

                        if (result.OrderItems == null)
                            result.OrderItems = new List<OrderItemDto>();

                        result.OrderItems.Add(orderItemDto);

                        return orderDto;
                    }, new { id }, splitOn: "Quantity");

                return result;
            }
        }

        /// <summary>
        /// Получение заказов с учетом фильтров
        /// </summary>
        /// <param name="customerId">Уникальный идентификатор пользователя</param>
        /// <param name="dateFrom">Дата начала фильтрации</param>
        /// <param name="dateTo">Дата окончания фильтрации</param>
        /// <returns></returns>
        public async Task<ICollection<OrderDto>> GetOrdersAsync(long? customerId = null, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                var sqlQueryBuilder = new StringBuilder(_baseOrdersSqlQuery);

                if (customerId != null || dateFrom != null || dateTo != null)
                    sqlQueryBuilder.Append(@" WHERE ");

                var filters = new List<string>();

                if (customerId != null)
                    filters.Add(@"o.CustomerId = @customerId");

                if (dateFrom != null)
                    filters.Add(@"o.CreationDateTimeUtc >= @dateFrom");

                if (dateTo != null)
                    filters.Add(@"o.CreationDateTimeUtc < @dateTo");

                AddFilters(sqlQueryBuilder, filters);

                var lookup = new Dictionary<long, OrderDto>();

                connection.Open();

                await connection.QueryAsync<OrderDto, OrderItemDto, OrderDto>(sqlQueryBuilder.ToString(),
                    (orderDto, orderItemDto) =>
                    {
                        if (lookup.ContainsKey(orderDto.Id) == false)
                            lookup.Add(orderDto.Id, orderDto);

                        var relevantOrderDto = lookup[orderDto.Id];

                        if (relevantOrderDto.OrderItems == null)
                            relevantOrderDto.OrderItems = new List<OrderItemDto>();

                        relevantOrderDto.OrderItems.Add(orderItemDto);

                        return orderDto;
                    }, new { customerId, dateFrom, dateTo }, splitOn: "Quantity");

                return lookup.Values.ToList();
            }
        }

        /// <summary>
        /// Добавление фильтров
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="filters"></param>
        private static void AddFilters(StringBuilder sb, IList<string> filters)
        {
            if (filters?.Count > 0)
            {
                if (filters.Count > 1)
                {
                    for (int i = 0; i < filters.Count - 1; i++)
                    {
                        sb.Append($"{filters[i]} AND ");
                    }
                }

                sb.Append($"{filters.Last()}");
            }
        }
    }
}