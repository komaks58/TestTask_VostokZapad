using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using OrderingService.Api.Application.Queries;
using OrderingService.Infrastructure;

namespace OrderingService.Tests.Mocks
{
    public class DbConnectionFactoryMock : IDbConnectionFactory, IDisposable
    {
        private readonly string _connectionString = "DataSource=:memory:";
        private readonly IDbConnection _connection;
        private readonly OrdersContext _context;

        public DbConnectionFactoryMock()
        {
            _connection = new SqliteConnection(_connectionString);
            _connection.Open();

            var options = new DbContextOptionsBuilder<OrdersContext>()
                .UseSqlite((DbConnection)_connection)
                .Options;

            _context = new OrdersContext(options);
            _context.Database.EnsureCreated();
        }

        public IDbConnection GetConnection()
        {
            return _connection;
        }

        public void Insert<T>(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                _context.Add(item);
            }

            _context.SaveChanges();
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
