using System.Threading.Tasks;
using AutoMapper;
using Virta.Api.Services.Interfaces;
using Virta.Data.Interfaces;
using Virta.Entities;
using Virta.Models;
using Virta.Extensions;
using System;
using Microsoft.AspNetCore.SignalR;
using Virta.Api.SignalR;
using Microsoft.AspNetCore.Http;

namespace Virta.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IHubContext<CustomerHub, ICustomerClient> _hubContext;

        public CustomerService(
            IMapper mapper,
            ICartRepository cartRepository,
            IWishlistRepository wishlistRepository,
            IHttpContextAccessor contextAccessor,
            IHubContext<CustomerHub, ICustomerClient> hubContext
        )
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
            _wishlistRepository = wishlistRepository;
            _contextAccessor = contextAccessor;
            _hubContext = hubContext;
        }

        protected HttpContext Context =>
            _contextAccessor.HttpContext;

        public async Task<bool> UpsertCartAsync(CartUpsert cart, Guid userId)
        {
            var cartToSave = _mapper.Map<Cart>(cart);

            cartToSave.UserId = userId;

            await _cartRepository.UpsertAsync(cartToSave);

            BroadcastUpdate(userId);

            return true;
        }

        public async Task<bool> UpsertWishlistAsync(WishlistUpsert wishlist, Guid userId)
        {
            var wishlistToSave = _mapper.Map<Wishlist>(wishlist);

            wishlistToSave.UserId = userId;

            await _wishlistRepository.UpsertAsync(wishlistToSave);

            BroadcastUpdate(userId);

            return true;
        }

        protected void BroadcastUpdate(Guid userId)
        {
            _hubContext.Clients.Clients(CustomerHub.ConnectedCustomers[userId]).OnCartUpdate();
        }
    }
}
