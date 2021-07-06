using System;
using System.Collections.Generic;

namespace Virta.Api.DTO
{
    public class OrderPLP
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<OrderProduct> Products { get; set; }

        public class OrderProduct
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public decimal Price { get; set; }
        }
    }
}
