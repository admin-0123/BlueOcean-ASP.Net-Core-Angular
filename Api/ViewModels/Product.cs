using System;
using System.Collections.Generic;

namespace Virta.ViewModels
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ProductAttributes> Attributes { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
