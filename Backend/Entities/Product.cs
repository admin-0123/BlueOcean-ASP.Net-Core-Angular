using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Virta.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public decimal Price { get; set; }
        #nullable enable
        public string? Description { get; set; }
        public string? SKU { get; set; }
        #nullable disable
        [Required]
        public ProductTypes Type { get; set; }
        [Required]
        public ProductVisibility Visible { get; set; }
        [Required]
        public bool Active { get; set; }

        public virtual ICollection<Product> AssociatedProducts { get; set; }
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }
        public virtual ICollection<Category> Categories { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public enum ProductTypes
        {
            Simple, // AssociatedProducts => NULL
            Bundle, // AssociatedProducts => Bundled products
            Configurable  // AssociatedProducts => Product variations
        }

        public enum ProductVisibility
        {
            Invisible, // Invisible
            PDP, // Only PDP
            PLP // PDP & PLP
        }
    }
}
