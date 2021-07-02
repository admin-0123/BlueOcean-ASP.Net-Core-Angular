using System;
using System.Collections.Generic;

namespace Virta.Models
{
    public class ProductUpsert
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<ProductAttributes> Attributes { get; set; }
        // public List<string> Images { get; set; }
        public List<Category> Categories { get; set; }

        public class ProductAttributes
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        public class Category
        {
            public string Title { get; set; }
            public string Value { get; set; }
        }
    }
}
