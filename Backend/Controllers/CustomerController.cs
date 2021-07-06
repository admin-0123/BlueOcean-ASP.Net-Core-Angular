using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Virta.Api.DTO;
using Virta.Api.Services.Interfaces;
using Virta.Data.Interfaces;
using Virta.Extensions;
using Virta.Models;

namespace Virta.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;
        private readonly IWishlistRepository _wishlistRepository;
        private readonly ICustomerService _customerService;

        public CustomerController(
            IMapper mapper,
            ICartRepository cartRepository,
            IWishlistRepository wishlistRepository,
            ICustomerService customerService
        )
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
            _wishlistRepository = wishlistRepository;
            _customerService = customerService;
        }

        [Authorize]
        [HttpGet("cart")]
        public async Task<IActionResult> GetCart()
        {
            var cart = await _cartRepository.GetAsync(User.GetUserId());

            if (cart == null)
                return BadRequest();

            return Ok(cart);
        }

        [Authorize]
        [HttpPost("cart")]
        public async Task<IActionResult> UpsertCart(CartDTO cartDTO)
        {
            var cartUpsert = _mapper.Map<CartUpsert>(cartDTO);

            if (await _customerService.UpsertCartAsync(cartUpsert, User.GetUserId()))
                return Ok();

            return BadRequest();
        }

        [Authorize]
        [HttpGet("wishlist")]
        public async Task<IActionResult> GetWishlist()
        {
            var wishlist = await _wishlistRepository.GetAsync(User.GetUserId());

            if (wishlist == null)
                return BadRequest();

            return Ok(wishlist);
        }

        [Authorize]
        [HttpPost("wishlist")]
        public async Task<IActionResult> UpsertWishlist(WishlistDTO wishlistDTO)
        {
            var wishlistUpsert = _mapper.Map<WishlistUpsert>(wishlistDTO);

            if (await _customerService.UpsertWishlistAsync(wishlistUpsert, User.GetUserId()))
                return Ok();

            return BadRequest();
        }
    }
}
