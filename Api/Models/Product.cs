using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VirtaApi.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        [Required]
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Attributes { get; set; }
        public string Images { get; set; }
    }
}
