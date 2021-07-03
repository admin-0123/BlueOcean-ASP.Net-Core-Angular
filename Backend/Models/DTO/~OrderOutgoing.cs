using System.Collections.Generic;
using Virta.Api.DTO;

namespace Virta.Api.DTO
{
    public class OrderOutgoing
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderProduct> Products { get; set; }
        public string UserId { get; set; }

        public class OrderProduct
        {
            public ProductPDP Product { get; set; }
            public decimal OrderPrice { get; set; }
            public int Quantity { get; set; }
        }
    }
}
