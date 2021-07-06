using System.Threading.Tasks;
using AutoMapper;
using Virta.Api.Services.Interfaces;
using Virta.Data.Interfaces;
using Virta.Entities;
using Virta.Models;
using Virta.Extensions;
using System;

namespace Virta.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;
        private readonly IWishlistRepository _wishlistRepository;

        public CustomerService(
            IMapper mapper,
            ICartRepository cartRepository,
            IWishlistRepository wishlistRepository
        )
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
            _wishlistRepository = wishlistRepository;
        }

        public async Task<bool> UpsertCartAsync(CartUpsert cart, Guid userId)
        {
            var cartToSave = _mapper.Map<Cart>(cart);

            cartToSave.UserId = userId;

            await _cartRepository.UpsertAsync(cartToSave);

            return true;
        }

        public async Task<bool> UpsertWishlistAsync(WishlistUpsert wishlist, Guid userId)
        {
            var wishlistToSave = _mapper.Map<Wishlist>(wishlist);

            wishlistToSave.UserId = userId;

            await _wishlistRepository.UpsertAsync(wishlistToSave);

            return true;
        }
    }
}
