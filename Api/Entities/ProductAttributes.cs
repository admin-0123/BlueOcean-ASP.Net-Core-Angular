using System.ComponentModel.DataAnnotations;

namespace Virta.Entities
{
    public class ProductAttributes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        [Required]
        public virtual Product Product { get; set; }
    }
}
