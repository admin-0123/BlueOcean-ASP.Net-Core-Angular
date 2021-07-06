using MongoDB.Driver;
using Virta.Data.Interfaces;
using Virta.Entities;

namespace Virta.Data
{
    public class WishlistRepository : MongoBaseRepository<Wishlist>, IWishlistRepository
    {
        private readonly IMongoClient _mongoClient;
        private readonly IClientSessionHandle _clientSessionHandle;

        public WishlistRepository(
            IMongoClient mongoClient,
            IClientSessionHandle clientSessionHandle
        ) : base(mongoClient, clientSessionHandle, nameof(Wishlist))
        {
            _mongoClient = mongoClient;
            _clientSessionHandle = clientSessionHandle;
        }
    }
}
