using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Virta.MVC.ViewModels
{
    public class ProductUpsertVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<ProductAttributesVM> Attributes { get; set; }
        public List<string> Images { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}
