using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingService.Api.Application.Settings
{
    /// <summary>
    /// Конфигурация приложения
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Настройки базы данных
        /// </summary>
        public DbSettings DbSettings { get; set; }
    }
}
