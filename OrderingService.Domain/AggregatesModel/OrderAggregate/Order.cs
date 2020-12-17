using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingService.Domain.AggregatesModel.OrderAggregate
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class Order : Entity, IAggregateRoot
    {
        /// <summary>
        /// Дата создания заказа
        /// </summary>
        public DateTime CreationDateTimeUtc { get; set; }

        /// <summary>
        /// Уникальный идентификатор пользователя, который осуществил заказ
        /// </summary>
        public long CustomerId { get; set; }

        /// <summary>
        /// Позиции заказа
        /// </summary>
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
