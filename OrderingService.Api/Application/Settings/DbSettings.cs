using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingService.Api.Application.Settings
{
    /// <summary>
    /// Настройки базы данных
    /// </summary>
    public class DbSettings
    {
        /// <summary>
        /// Строка подключения к базе данных
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
