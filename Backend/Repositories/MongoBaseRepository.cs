using Humanizer;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using Virta.Data.Interfaces;
using Virta.Entities;

namespace Virta.Data
{
    public abstract class MongoBaseRepository<T> : IMongoBaseRepository<T> where T : MongoBaseDocument
    {
        private const string DATABASE = "Virta";
        private readonly IMongoClient _mongoClient;
        private readonly IClientSessionHandle _clientSessionHandle;
        private readonly string _collection;

        public MongoBaseRepository(
            IMongoClient mongoClient,
            IClientSessionHandle clientSessionHandle,
            string collection
        )
        {
            _mongoClient = mongoClient;
            _clientSessionHandle = clientSessionHandle;
            _collection = collection.Pluralize();

            if (!_mongoClient.GetDatabase(DATABASE).ListCollectionNames().ToList().Contains(_collection))
                _mongoClient.GetDatabase(DATABASE).CreateCollection(_collection);
        }

        protected IMongoCollection<T> Collection =>
            _mongoClient.GetDatabase(DATABASE).GetCollection<T>(_collection);

        public async Task<T> GetAsync(Guid id)
        {
            return await Collection.Find(d => d.UserId == id).FirstOrDefaultAsync();
        }

        public async Task UpsertAsync(T entity)
        {
            if (entity.UserId == Guid.Empty)
                throw new InvalidOperationException("UserId cannot be empty");

            await Collection.ReplaceOneAsync(
                _clientSessionHandle,
                d => d.UserId == entity.UserId,
                entity,
                new ReplaceOptions { IsUpsert = true }
            );
        }

        public async Task DeleteAsync(Guid id)
        {
            await Collection.DeleteOneAsync(
                _clientSessionHandle,
                entity => entity.UserId == id
            );
        }
    }
}
