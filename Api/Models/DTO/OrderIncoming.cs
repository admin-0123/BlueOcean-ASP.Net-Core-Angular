using System;
using System.Collections.Generic;

namespace Virta.Api.Models.DTO
{
    public class OrderIncoming
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<OrderProduct> Products { get; set; }

        public class OrderProduct
        {
            public Guid Id { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }
    }
}
