using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingService.Api.Application.Queries.Order.Models
{
    public class OrderItemDto
    {
        public string Name { get; set; }

        public int Quantity { get; set; }

        public long Price { get; set; }
    }
}
