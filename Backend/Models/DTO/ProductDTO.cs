using System;
using System.Collections.Generic;

namespace Virta.Api.DTO
{
    public class ProductDTO
    {
#nullable enable
        public Guid? Id { get; set; }
        public string? Description { get; set; }
        public string? SKU { get; set; }
        public virtual List<ProductAttributeDTO>? Attributes { get; set; }
        public virtual List<ProductImageDTO>? Images { get; set; }
        public List<Guid>? AssociatedProducts { get; set; }
#nullable disable
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Type { get; set; }
        public int Visible { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<CategoryDTO> Categories { get; set; }
    }
}
