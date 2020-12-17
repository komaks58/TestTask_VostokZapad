using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using OrderingService.Api.Application.Queries.Order;
using OrderingService.Api.Application.Queries.Order.Models;
using OrderingService.Domain.AggregatesModel.CustomerAggregate;
using OrderingService.Domain.AggregatesModel.OrderAggregate;
using OrderingService.Tests.Mocks;

namespace OrderingService.Tests
{
    public class OrderQueriesTest : IDisposable

    {
        private DbConnectionFactoryMock _dbConnectionFactory;
        private IOrderQueries _orderQueries;

        #region seed data

        private readonly ICollection<Customer> _customersSeed = new List<Customer>()
    {
        new Customer
        {
            Id = 1,
            Name = "Dmitriy",
            BirthDate = DateTime.Parse("2016-06-15T18:00:00.000Z")
        },
        new Customer
        {
            Id = 2,
            Name = "Dima",
            BirthDate = DateTime.Parse("2016-06-15T18:00:00.000Z")
        },
    };

        private readonly ICollection<Product> _productsSeed = new List<Product>()
    {
        new Product
        {
            Id = 1,
            Name = "Computer",
            Description = "Компьютер",
            Price = 100
        },
        new Product
        {
            Id = 2,
            Name = "Keyboard",
            Description = "Клавиатура",
            Price = 10
        },
        new Product
        {
            Id = 3,
            Name = "Monitor",
            Description = "Монитор",
            Price = 50
        },
        new Product
        {
            Id = 4,
            Name = "Laptop",
            Description = "Ноутбук",
            Price = 80
        },
    };

        private readonly ICollection<Order> _ordersSeed = new List<Order>()
    {
        new Order
        {
            Id = 1,
            CreationDateTimeUtc = DateTime.Parse("2010-06-15T18:00:00.000Z"),
            CustomerId = 1,
            OrderItems = new List<OrderItem>()
            {
                new OrderItem
                {
                    Id = 1,
                    OrderId = 1,
                    ProductId = 1,
                    ProductsQuantity = 1
                },
            }
        },
        new Order
        {
            Id = 2,
            CreationDateTimeUtc = DateTime.Parse("2011-06-15T18:00:00.000Z"),
            CustomerId = 2,
            OrderItems = new List<OrderItem>()
            {
                new OrderItem
                {
                    Id = 2,
                    OrderId = 2,
                    ProductId = 2,
                    ProductsQuantity = 2
                },
                new OrderItem
                {
                    Id = 3,
                    OrderId = 2,
                    ProductId = 3,
                    ProductsQuantity = 3
                },
            }
        },
        new Order
        {
            Id = 3,
            CreationDateTimeUtc = DateTime.Parse("2012-06-15T18:00:00.000Z"),
            CustomerId = 2,
            OrderItems = new List<OrderItem>()
            {
                new OrderItem
                {
                    Id = 4,
                    OrderId = 3,
                    ProductId = 4,
                    ProductsQuantity = 4
                },
            }
        },
    };

        #endregion

        public OrderQueriesTest()
        {

        }

        [SetUp]
        public void Setup()
        {
            _dbConnectionFactory = new DbConnectionFactoryMock();

            _orderQueries = new OrderQueries(_dbConnectionFactory);
        }

        [Test]
        public async Task Get_All_ReturnsAll()
        {
            // Arrange
            SeedData();

            // Act
            var result = await _orderQueries.GetOrdersAsync();

            var expectedResult = new List<OrderDto>()
        {
            new OrderDto
            {
                Id = 1,
                CreationDateTimeUtc = DateTime.Parse("2010-06-15T18:00:00.000Z"),
                CustomerId = 1,
                OrderItems = new List<OrderItemDto>()
                {
                    new OrderItemDto
                    {
                        Name = "Computer",
                        Price = 100,
                        Quantity = 1
                    },
                }
            },
            new OrderDto
            {
                Id = 2,
                CreationDateTimeUtc = DateTime.Parse("2011-06-15T18:00:00.000Z"),
                CustomerId = 2,
                OrderItems = new List<OrderItemDto>()
                {
                    new OrderItemDto
                    {
                        Name = "Keyboard",
                        Price = 10,
                        Quantity = 2
                    },
                    new OrderItemDto
                    {
                        Name = "Monitor",
                        Price = 50,
                        Quantity = 3
                    },
                }
            },
            new OrderDto
            {
                Id = 3,
                CreationDateTimeUtc = DateTime.Parse("2012-06-15T18:00:00.000Z"),
                CustomerId = 2,
                OrderItems = new List<OrderItemDto>()
                {
                    new OrderItemDto
                    {
                        Name = "Laptop",
                        Price = 80,
                        Quantity = 4
                    },
                }
            },
        };

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Get_ByCustomerId_ReturnsOne()
        {
            // Arrange
            SeedData();

            // Act
            var result = await _orderQueries.GetOrdersAsync(customerId: 1);

            var expectedResult = new List<OrderDto>()
        {
            new OrderDto
            {
                Id = 1,
                CreationDateTimeUtc = DateTime.Parse("2010-06-15T18:00:00.000Z"),
                CustomerId = 1,
                OrderItems = new List<OrderItemDto>()
                {
                    new OrderItemDto
                    {
                        Name = "Computer",
                        Price = 100,
                        Quantity = 1
                    },
                }
            },
        };

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Get_ByCustomerId_ReturnsZero()
        {
            // Arrange
            SeedData();

            // Act
            var result = await _orderQueries.GetOrdersAsync(customerId: 5);

            var expectedResult = new List<OrderDto>();

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Get_ByDateFrom_ReturnsOne()
        {
            // Arrange
            SeedData();

            // Act
            var result = await _orderQueries.GetOrdersAsync(dateFrom: DateTime.Parse("2012-05-15T18:00:00.000Z"));

            var expectedResult = new List<OrderDto>()
        {
            new OrderDto
            {
                Id = 3,
                CreationDateTimeUtc = DateTime.Parse("2012-06-15T18:00:00.000Z"),
                CustomerId = 2,
                OrderItems = new List<OrderItemDto>()
                {
                    new OrderItemDto
                    {
                        Name = "Laptop",
                        Price = 80,
                        Quantity = 4
                    },
                }
            },
        };

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Get_ByDateFrom_ReturnsZero()
        {
            // Arrange
            SeedData();

            // Act
            var result = await _orderQueries.GetOrdersAsync(dateFrom: DateTime.Parse("2014-05-15T18:00:00.000Z"));

            var expectedResult = new List<OrderDto>();

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Get_ByDateTo_ReturnsOne()
        {
            // Arrange
            SeedData();

            // Act
            var result = await _orderQueries.GetOrdersAsync(dateTo: DateTime.Parse("2010-07-15T18:00:00.000Z"));

            var expectedResult = new List<OrderDto>()
        {
            new OrderDto
            {
                Id = 1,
                CreationDateTimeUtc = DateTime.Parse("2010-06-15T18:00:00.000Z"),
                CustomerId = 1,
                OrderItems = new List<OrderItemDto>()
                {
                    new OrderItemDto
                    {
                        Name = "Computer",
                        Price = 100,
                        Quantity = 1
                    },
                }
            },
        };

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Get_ByDateTo_ReturnsZero()
        {
            // Arrange
            SeedData();

            // Act
            var result = await _orderQueries.GetOrdersAsync(dateTo: DateTime.Parse("2009-07-15T18:00:00.000Z"));

            var expectedResult = new List<OrderDto>();

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Get_ByCustomerIdAndDates_ReturnsOne()
        {
            // Arrange
            SeedData();

            // Act
            var result = await _orderQueries.GetOrdersAsync(customerId: 1,
                dateFrom: DateTime.Parse("2009-07-15T18:00:00.000Z"),
                dateTo: DateTime.Parse("2018-07-15T18:00:00.000Z"));

            var expectedResult = new List<OrderDto>()
        {
            new OrderDto
            {
                Id = 1,
                CreationDateTimeUtc = DateTime.Parse("2010-06-15T18:00:00.000Z"),
                CustomerId = 1,
                OrderItems = new List<OrderItemDto>()
                {
                    new OrderItemDto
                    {
                        Name = "Computer",
                        Price = 100,
                        Quantity = 1
                    },
                }
            },
        };

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        private void SeedData()
        {
            _dbConnectionFactory.Insert(_customersSeed);
            _dbConnectionFactory.Insert(_productsSeed);
            _dbConnectionFactory.Insert(_ordersSeed);
        }

        public void Dispose()
        {
            _dbConnectionFactory?.Dispose();
        }
    }
}