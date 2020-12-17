using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderingService.Api.Application.Queries.Order;

namespace OrderingService.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{apiVersion}/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IOrderQueries _orderQueries;

        public CustomersController(IOrderQueries orderQueries)
        {
            _orderQueries = orderQueries ?? throw new ArgumentNullException(nameof(orderQueries));
        }


        /// <summary>
        /// Получение заказов пользователя с учетом фильтров
        /// </summary>
        /// <param name="customerId">Уникальный идентификатор пользователя</param>
        /// <param name="dateFrom">Дата начала фильтрации</param>
        /// <param name="dateTo">Дата окончания фильтрации</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{customerId}/orders")]
        public async Task<ActionResult> GetAll([FromQuery] long customerId, [FromQuery]DateTime? dateFrom = null, [FromQuery] DateTime? dateTo = null)
        {
            var ordersDto = await _orderQueries.GetOrdersAsync(customerId: customerId, dateFrom: dateFrom, dateTo: dateTo);

            return Ok(ordersDto);
        }
    }
}