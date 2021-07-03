using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Virta.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        [Required]
        public string URL { get; set; }
        public bool Primary { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
