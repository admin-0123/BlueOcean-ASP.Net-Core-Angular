using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Virta.Api.DTO;
using Virta.Data.Interfaces;
using Virta.Models;
using Virta.Services.Interfaces;

namespace Virta.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _repo;
        private readonly ICategoriesRepository _categoriesRepo;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductsController(
            IMapper mapper,
            IProductsRepository repo,
            ICategoriesRepository categoriesRepo,
            IProductService productService
        )
        {
            _mapper = mapper;
            _repo = repo;
            _categoriesRepo = categoriesRepo;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _repo.GetProducts();

            if (products == null)
                return BadRequest();

            return Ok(products);
        }

        // [HttpGet("categories")]
        // public async Task<IActionResult> GetProducts([FromQuery(Name = "category")] List<string> categories)
        // {
        //     var products = await _repo.GetProducts(categories);

        //     if (products == null)
        //         return Ok("False");

        //     var response = _mapper.Map<IEnumerable<ProductPLP>>(products);

        //     return Ok(response);

        // }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var product = await _repo.GetProduct(id);

            if (product == null)
                return BadRequest();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(ProductDTO productDTO)
        {
            var product = _mapper.Map<ProductUpsert>(productDTO);

            if (await _productService.Upsert(product))
                return Ok();

            return BadRequest();
        }

        [HttpGet("seed")]
        public async Task<IActionResult> Seed()
        {
            var rawData = await System.IO.File.ReadAllTextAsync("bsData/products.json");
            var products = JsonSerializer.Deserialize<IEnumerable<ProductDTO>>(rawData);

            foreach (var product in products)
                await Upsert(product);

            return Ok();
        }
    }
}
