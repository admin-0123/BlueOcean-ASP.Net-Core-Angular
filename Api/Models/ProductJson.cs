using System.Collections.Generic;

namespace VirtaApi.Models
{
    public class ProductJson
    {
        public string Title { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public ICollection<ProductAttributes> Attributes { get; set; }
        public List<string> Images { get; set; }

    }
}
