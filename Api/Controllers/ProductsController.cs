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
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;
        public ProductsController(
            IMapper mapper,
            IProductsRepository repo,
            ICategoriesRepository categoriesRepository
        )
        {
            _mapper = mapper;
            _repo = repo;
            _categoriesRepository = categoriesRepository;
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
        public async Task<IActionResult> AddProduct([FromForm] Product product)
        {
            _repo.Add<Product>(product);

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

            var productGuids = new List<Guid>();

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

                var id = new Guid();
                product.Id = id;

                _repo.Add<Product>(
                    new Product{
                        Id = id,
                        Title = product.Title,
                        Price = decimal.Parse(product.Price),
                        Description = product.Description,
                        Attributes = attributes,
                        Images = JsonSerializer.Serialize(product.Images)
                    }
                );
            }

            if(await _repo.SaveAll()) {
                foreach (var product in products)
                {
                    var categories = new List<Category>();

                    product.Categories.ForEach(
                        c => categories.Add(
                            new Category {
                                Value = c.Value,
                                Title = textInfo.ToTitleCase(c.Value)
                            }
                        )
                    );

                    var productFromDb = await _repo.GetProduct(product.Id);

                    categories.ForEach(
                        c => {
                            c.Products.Add(productFromDb);
                            _categoriesRepository.Update<Category>(c);
                        }
                    );
                }

                if(await _repo.SaveAll())
                    return Ok("True");
            }

            return Ok("False");
        }
    }
}
