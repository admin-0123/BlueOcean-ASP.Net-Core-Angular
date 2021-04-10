using System;
using System.Collections.Generic;

namespace Virta.Api.DTO
{
    public class ProductPLP
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public List<string> Images { get; set; }
    }
}
