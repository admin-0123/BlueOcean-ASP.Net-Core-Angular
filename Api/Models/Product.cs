using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VirtaApi.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ProductAttributes> Attributes { get; set; }
        public string Images { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
