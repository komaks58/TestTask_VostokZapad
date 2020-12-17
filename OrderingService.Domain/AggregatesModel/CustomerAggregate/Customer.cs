using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingService.Domain.AggregatesModel.CustomerAggregate
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class Customer : Entity, IAggregateRoot
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDate { get; set; }
    }
}
