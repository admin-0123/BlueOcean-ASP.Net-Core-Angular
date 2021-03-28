using System;
using System.Collections.Generic;
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
        private readonly IMapper _mapper;
        public ProductsController(
            IProductsRepository repo,
            IMapper mapper
        )
        {
            _repo = repo;
            _mapper = mapper;
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

            foreach (var product in products)
            {
                _repo.Add<Product>(
                    new Product{
                        Id = new Guid(),
                        Title = product.Title,
                        Price = decimal.Parse(product.Price),
                        Description = product.Description,
                        Attributes = product.Attributes,
                        Images = JsonSerializer.Serialize(product.Images)
                    }
                );
            }

            if(await _repo.SaveAll())
                return Ok("True");

            return Ok("False");
        }
    }
}
