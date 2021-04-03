using System;
using System.Collections.Generic;

namespace Virta.MVC.ViewModels
{
    public class ProductPLP
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
        public List<ProductAttributes> Attributes { get; set; }
        public List<Category> Categories { get; set; }
    }
}
