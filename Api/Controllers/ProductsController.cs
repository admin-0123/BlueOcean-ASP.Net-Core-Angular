using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VirtaApi.Data.Interfaces;
using VirtaApi.Models;

namespace VirtaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _repo;
        public ProductsController(IProductsRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _repo.GetProducts());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducts(Guid id)
        {
            return Ok(await _repo.GetProduct(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] Product product)
        {
            _repo.Add<Product>(product);

            if(await _repo.SaveAll())
                return Ok("True");

            return Ok("False");
        }
    }
}
