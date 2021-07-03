using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Virta.Entities
{
    public class ProductAttribute
    {
        public int Id { get; set; }
        [Required]
        public string Value { get; set; }
        public int Priority { get; set; }

        public virtual Attribute Attribute { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
