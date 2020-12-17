using System;
using System.Data;
using System.Data.SqlClient;

namespace OrderingService.Api.Application.Queries
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;
        public DbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        /// <summary>
        /// Установление подкючения к базе
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            var connection = new SqlConnection(_connectionString);
            return connection;
        }
    }
}