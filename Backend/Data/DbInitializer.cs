using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using AutoMapper;
using Virta.Api.DTO;
using Virta.Entities;

namespace Virta.Data
{
    public static class DbInitializer
    {
        public async static void Initialize(DataContext context, Mapper mapper)
        {
            if (!context.Categories.Any())
            {
                var rawData = await System.IO.File.ReadAllTextAsync("bsData/categories.json");
                var categories = JsonSerializer.Deserialize<IEnumerable<CategoryDTO>>(rawData);

                foreach (var category in categories)
                    context.Add<Category>(mapper.Map<Category>(category));

                context.SaveChanges();
            }

            if (!context.Attributes.Any())
            {
                var rawData = await System.IO.File.ReadAllTextAsync("bsData/attributes.json");
                var attributes = JsonSerializer.Deserialize<IEnumerable<AttributeDTO>>(rawData);

                foreach (var attribute in attributes)
                    context.Add<Attribute>(mapper.Map<Attribute>(attribute));

                context.SaveChanges();
            }

            if (!context.Products.Any())
            {
                var rawData = await System.IO.File.ReadAllTextAsync("bsData/attributes.json");
                var products = JsonSerializer.Deserialize<IEnumerable<ProductDTO>>(rawData);

                foreach (var product in products)
                    context.Add<Product>(mapper.Map<Product>(product));

                context.SaveChanges();
            }
        }
    }
}
