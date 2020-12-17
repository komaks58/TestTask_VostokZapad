using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingService.Domain.AggregatesModel
{
    /// <summary>
    /// Базовый класс сущности
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public long Id { get; set; }
    }
}
