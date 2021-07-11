using System;
using System.Collections.Generic;

namespace Virta.Models
{
    public class CartUpsert
    {
        public IEnumerable<CartItemUpsert> Products { get; set; }

        public class CartItemUpsert
        {
            public Guid Id { get; set; }
            public int Quantity { get; set; }
            public string Title { get; set; }
            public decimal Price { get; set; }
            public string[] Images { get; set; }
        }
    }
}
