using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Virta.Data.Interfaces;
using Virta.Entities;
using Humanizer;

namespace Virta.Data
{
    public class MongoBaseRepository<T> : IMongoBaseRepository<T> where T : MongoBaseDocument
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

        public async Task<T> GetAsync(string Id)
        {
            var filter = Builders<T>.Filter.Eq(s => s.Id, ObjectId.Parse(Id));
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task InsertAsync(T entity)
        {
            entity.Id = ObjectId.GenerateNewId();
            await Collection.InsertOneAsync(_clientSessionHandle, entity);
        }

        public async Task DeleteAsync(string id)
        {
            await Collection.DeleteOneAsync(
                _clientSessionHandle,
                entity => entity.Id == ObjectId.Parse(id)
            );
        }
    }
}
