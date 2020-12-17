using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingService.Api.Application.Queries.Order.Models
{
    public class OrderDto
    {
        public long Id { get; set; }

        public long CustomerId { get; set; }

        public DateTime CreationDateTimeUtc { get; set; }

        public ICollection<OrderItemDto> OrderItems { get; set; }
    }
}
