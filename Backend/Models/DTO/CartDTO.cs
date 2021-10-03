using System;
using System.Collections.Generic;

namespace Virta.Api.DTO
{
    public class CartDTO
    {
        public IEnumerable<CartItemDTO> Products { get; set; }

        public class CartItemDTO
        {
            public Guid Id { get; set; }
            public int Quantity { get; set; }
            public string Title { get; set; }
            public decimal Price { get; set; }
            public string[] Images { get; set; }
            public string Url { get; set; }
        }
    }
}
