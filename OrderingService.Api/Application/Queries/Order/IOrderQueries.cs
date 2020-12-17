using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderingService.Api.Application.Queries.Order.Models;

namespace OrderingService.Api.Application.Queries.Order
{
    public interface IOrderQueries
    {
        /// <summary>
        /// Получение заказа по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OrderDto> GetOrderAsync(long id);

        /// <summary>
        /// Получение заказов с учетом фильтров
        /// </summary>
        /// <param name="customerId">Уникальный идентификатор пользователя</param>
        /// <param name="dateFrom">Дата начала фильтрации</param>
        /// <param name="dateTo">Дата окончания фильтрации</param>
        /// <returns></returns>
        Task<ICollection<OrderDto>> GetOrdersAsync(long? customerId = null, DateTime? dateFrom = null, DateTime? dateTo = null);
    }
}
