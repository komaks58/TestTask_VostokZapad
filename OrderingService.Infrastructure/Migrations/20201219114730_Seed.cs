using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderingService.Infrastructure.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO customers
                (Name, BirthDate)
                VALUES 
                ('Dmitriy', '2016-06-15T18:00:00.000Z'),
                ('Dima', '2017-06-15T18:00:00.000Z'),
                ('Dimon', '2018-06-15T18:00:00.000Z')");

            migrationBuilder.Sql(@"
                INSERT INTO products
                (Name, Description, Price)
                VALUES 
                ('Computer', 'Компьютер', 100),
                ('Keyboard', 'Клавиатура', 10),
                ('Monitor', 'Монитор', 50),
                ('Laptop', 'Ноутбук', 80)");

            migrationBuilder.Sql(@"
                INSERT INTO orders
                (CustomerId, CreationDateTimeUtc)
                VALUES 
                (1, '2010-06-15T18:00:00.000Z'),
                (1, '2011-06-15T18:00:00.000Z'),
                (1, '2012-06-15T18:00:00.000Z'),
                (2, '2013-06-15T18:00:00.000Z'),
                (2, '2014-06-15T18:00:00.000Z')");

            migrationBuilder.Sql(@"
                INSERT INTO orderItems
                (OrderId, ProductId, ProductsQuantity)
                VALUES 
                (1, 1, 1),
                (1, 2, 12),
                (2, 3, 1),
                (3, 3, 1),
                (3, 4, 1),
                (4, 1, 5),
                (4, 2, 7),
                (5, 4, 5)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM orderItems", true);
            migrationBuilder.Sql("DELETE FROM orders", true);
            migrationBuilder.Sql("DELETE FROM products", true);
            migrationBuilder.Sql("DELETE FROM customers", true);
        }
    }
}
