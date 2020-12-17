using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingService.Domain.AggregatesModel.OrderAggregate
{
    /// <summary>
    /// Продукт
    /// </summary>
    public class Product : Entity
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public long Price { get; set; }
    }
}
