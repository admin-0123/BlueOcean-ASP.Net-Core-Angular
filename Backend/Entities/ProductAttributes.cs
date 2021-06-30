using System;
using System.ComponentModel.DataAnnotations;

namespace Virta.Entities
{
    public class ProductAttributes
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Value { get; set; }
        [Required]
        public int Priority { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
