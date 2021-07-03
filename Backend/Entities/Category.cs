using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Virta.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
