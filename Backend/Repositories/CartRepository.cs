using MongoDB.Driver;
using Virta.Data.Interfaces;
using Virta.Entities;

namespace Virta.Data
{
    public class CartRepository : MongoBaseRepository<Cart>, ICartRepository
    {
        private readonly IMongoClient _mongoClient;
        private readonly IClientSessionHandle _clientSessionHandle;

        public CartRepository(
            IMongoClient mongoClient,
            IClientSessionHandle clientSessionHandle
        ) : base(mongoClient, clientSessionHandle, nameof(Cart))
        {
            _mongoClient = mongoClient;
            _clientSessionHandle = clientSessionHandle;
        }
    }
}
