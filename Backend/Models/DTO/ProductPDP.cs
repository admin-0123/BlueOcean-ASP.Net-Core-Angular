using System;
using System.Collections.Generic;

namespace Virta.Api.DTO
{
    public class ProductPDP
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<ProductAttributesDTO> Attributes { get; set; }
        // public List<string> Images { get; set; }
        public List<CategoryDTO> Categories { get; set; }

    }
}
