using System;
using System.Collections.Generic;

namespace Virta.Api.DTO
{
    public class OrderUpsert
    {
        public int Id { get; set; }
        public decimal ShippingCost { get; set; }
        public List<OrderProduct> Products { get; set; }
        public string UserEmail { get; set; }

        public class OrderProduct
        {
            public Guid id { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
        }
    }
}
