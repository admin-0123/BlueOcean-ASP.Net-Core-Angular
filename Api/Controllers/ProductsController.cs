using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VirtaApi.Data.Interfaces;
using VirtaApi.DTO;
using VirtaApi.Models;

namespace VirtaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _repo;
        private readonly ICategoriesRepository _categoriesRepo;
        private readonly IMapper _mapper;
        public ProductsController(
            IMapper mapper,
            IProductsRepository repo,
            ICategoriesRepository categoriesRepo
        )
        {
            _mapper = mapper;
            _repo = repo;
            _categoriesRepo = categoriesRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _repo.GetProducts();

            if(products == null)
                return Ok("False");

            var response = _mapper.Map<IEnumerable<ProductPLP>>(products);

            return Ok(response);

        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetProducts([FromQuery(Name="category")] List<string> categories)
        {
            var products = await _repo.GetProducts(categories);

            if(products == null)
                return Ok("False");

            var response = _mapper.Map<IEnumerable<ProductPLP>>(products);

            return Ok(response);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await _repo.GetProduct(id);

            if(product == null)
                return Ok("False");

            var response = _mapper.Map<ProductPDP>(product);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductPDP product)
        {
            var productToSave = _mapper.Map<Product>(product);

            productToSave = await SetCategorise(productToSave);

            _repo.Add<Product>(productToSave);

            if(await _repo.SaveAll())
                return Ok("True");

            return Ok("False");
        }

        [HttpGet("seed")]
        public async Task<IActionResult> Seed()
        {
            var productData = await System.IO.File.ReadAllTextAsync("productData.json");
            var products = JsonSerializer.Deserialize<List<ProductJson>>(productData);

            if (products == null)
                return Ok("Null");

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            var productsToSave = new List<Product>();

            foreach (var product in products)
            {
                var attributes = new List<ProductAttributes>();
                product.Attributes.ForEach(
                    a => attributes.Add(
                        new ProductAttributes {
                            Name = a.Name,
                            Value = a.Value
                        }
                    )
                );

                var categories = new List<Category>();
                product.Categories.ForEach(
                    c => categories.Add(
                        new Category {
                            Value = c.Value,
                            Title = textInfo.ToTitleCase(c.Value)
                        }
                    )
                );

                product.Id = Guid.NewGuid();

                productsToSave.Add(
                    new Product{
                        Id = product.Id,
                        Title = product.Title,
                        Price = decimal.Parse(product.Price),
                        Description = product.Description,
                        Attributes = attributes,
                        Categories = categories,
                        Images = JsonSerializer.Serialize(product.Images)
                    }
                );
            }

            var l = await GetCategoriseSeed(productsToSave);
            l.ForEach(
                p => _repo.Add<Product>(p)
            );

            if(await _repo.SaveAll())
                return Ok("True");

            return Ok("False");
        }

        private async Task<List<Product>> GetCategoriseSeed(List<Product> products)
        {
            var newProducts = new List<Product>();

            foreach (var product in products)
            {
                var newProduct = product;
                var newCategories = new List<Category>();

                foreach (var category in product.Categories)
                {
                    newCategories.Add(await _categoriesRepo.GetCategory(category.Value));
                }

                newProduct.Categories = newCategories;
                newProducts.Add(newProduct);
            }

            return newProducts;
        }

        private async Task<Product> SetCategorise(Product product)
        {
            var newCategories = new List<Category>();

            foreach (var category in product.Categories)
            {
                newCategories.Add(await _categoriesRepo.GetCategory(category.Value));
            }

            product.Categories = newCategories;

            return product;
        }
    }
}
