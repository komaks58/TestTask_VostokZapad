using System.Collections.Generic;

namespace OrderingService.Domain.AggregatesModel.OrderAggregate
{
    /// <summary>
    /// Позиция заказа
    /// </summary>
    public class OrderItem : Entity
    {
        /// <summary>
        /// Уникальный идентификатор заказанного продукта
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// Уникальный идентификатор заказа
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        /// Количество заказанных продуктов
        /// </summary>
        public int ProductsQuantity { get; set; }
    }
}