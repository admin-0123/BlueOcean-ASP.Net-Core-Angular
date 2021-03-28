using System;
using System.Collections.Generic;

namespace VirtaApi.DTO
{
    public class ProductJson
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public List<ProductAttributesDTO> Attributes { get; set; }
        public List<string> Images { get; set; }
        public List<CategoryDTOJson> Categories { get; set; }
    }

    public class CategoryDTOJson {
        public string Value { get; set; }
    }
}
