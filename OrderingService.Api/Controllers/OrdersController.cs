using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderingService.Api.Application.Queries.Order;
using OrderingService.Api.Application.Queries.Order.Models;

namespace OrderingService.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{apiVersion}/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderQueries _orderQueries;

        public OrdersController(IOrderQueries orderQueries)
        {
            _orderQueries = orderQueries ?? throw new ArgumentNullException(nameof(orderQueries));
        }

        /// <summary>
        /// Получение заказа по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор заказа</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(long id)
        {
            var orderDto = await _orderQueries.GetOrderAsync(id);

            return Ok(orderDto);
        }

        /// <summary>
        /// Получение заказов с учетом фильтров
        /// </summary>
        /// <param name="dateFrom">Дата начала фильтрации</param>
        /// <param name="dateTo">Дата окончания фильтрации</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery]DateTime? dateFrom = null, [FromQuery] DateTime? dateTo = null)
        {
            var ordersDto = await _orderQueries.GetOrdersAsync(dateFrom: dateFrom, dateTo: dateTo);

            return Ok(ordersDto);
        }
    }
}