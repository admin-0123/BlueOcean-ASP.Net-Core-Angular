using System;
using System.Collections.Generic;

namespace Virta.Models
{
    public class ProductAttributeUpsert
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int Priority { get; set; }
    }
}
