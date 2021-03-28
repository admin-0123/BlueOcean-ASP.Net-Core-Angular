using System.Collections.Generic;

namespace VirtaApi.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
