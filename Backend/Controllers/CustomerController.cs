using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Virta.Data.Interfaces;
using Virta.Entities;

namespace Virta.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;

        public CustomerController(
            IMapper mapper,
            ICartRepository cartRepository
        )
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(string Id)
        {
            var cart = await _cartRepository.GetAsync(Id);

            if (cart == null)
                return BadRequest();

            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Cart cart)
        {
            await _cartRepository.InsertAsync(cart);

            return Ok();
        }
    }
}
