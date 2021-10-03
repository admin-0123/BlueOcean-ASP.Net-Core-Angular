using System;
using System.Collections.Generic;

namespace Virta.Api.DTO
{
    public class WishlistDTO
    {
        public IEnumerable<WishlistItemDTO> Products { get; set; }

        public class WishlistItemDTO
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public decimal Price { get; set; }
            public string[] Images { get; set; }
            public string Url { get; set; }
        }
    }
}
