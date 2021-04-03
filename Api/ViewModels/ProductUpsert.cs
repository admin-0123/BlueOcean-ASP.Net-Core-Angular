using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Virta.MVC.ViewModels
{
    public class ProductUpsert
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<ProductAttributes> Attributes { get; set; }
        public string[] Categories { get; set; }
    }
}
