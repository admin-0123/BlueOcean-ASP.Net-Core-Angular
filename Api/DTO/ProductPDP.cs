using System;
using System.Collections.Generic;
using VirtaApi.Models;

namespace VirtaApi.DTO
{
    public class ProductPDP
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ProductAttributes> Attributes { get; set; }
        public List<string> Images { get; set; }
    }
}
