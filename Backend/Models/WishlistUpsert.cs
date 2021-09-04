using System;
using System.Collections.Generic;

namespace Virta.Models
{
    public class WishlistUpsert
    {
        public IEnumerable<WishlistItemUpsert> Products { get; set; }

        public class WishlistItemUpsert
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public decimal Price { get; set; }
            public string[] Images { get; set; }
        }
    }
}
