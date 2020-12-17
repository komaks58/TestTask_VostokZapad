using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingService.Api.Application.Queries
{
    public interface IDbConnectionFactory
    {
        /// <summary>
        /// Установление подкючения к базе
        /// </summary>
        /// <returns></returns>
        IDbConnection GetConnection();
    }
}
