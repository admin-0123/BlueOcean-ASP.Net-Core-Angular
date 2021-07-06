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
        }
    }
}
