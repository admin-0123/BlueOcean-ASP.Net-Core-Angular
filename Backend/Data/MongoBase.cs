using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Virta.Data
{
    public class MongoBase<T> where T : BsonDocument
    {
        private const string DATABASE = "Virta";
        private readonly IMongoClient _mongoClient;
        private readonly IClientSessionHandle _clientSessionHandle;
        private readonly string _collection;
        public MongoBase(
            IMongoClient mongoClient,
            IClientSessionHandle clientSessionHandle,
            string collection
        )
        {
            _mongoClient = mongoClient;
            _clientSessionHandle = clientSessionHandle;
            _collection = collection;
            if (!_mongoClient.GetDatabase(DATABASE).ListCollectionNames().ToList().Contains(collection))
                _mongoClient.GetDatabase(DATABASE).CreateCollection(collection);
            // var db = _client.GetDatabase("Virta");
            // var collection = db.GetCollection<BsonDocument>("C");
            // collection.
        }

        protected virtual IMongoCollection<T> Collection =>
            _mongoClient.GetDatabase(DATABASE).GetCollection<T>(_collection);

        public async Task InsertAsync(T entity)
        {
            await Collection.InsertOneAsync(_clientSessionHandle, entity);
        }

        // public async Task UpdateAsync(T entity)
        // {
        //     Expression<Func<T, string>> func = f => f.Id;
        //     var value = (string) entity.GetType().GetProperty(func.Body.ToString().Split(".")[1]).GetValue(entity, null);
        //     var filter = Builders<T>.Filter.Eq(func, value);

        //     if (entity != null)
        //         await Collection.ReplaceOneAsync(_clientSessionHandle, filter, entity);
        // }

        public async Task DeleteAsync(string id)
        {
            await Collection.DeleteOneAsync(_clientSessionHandle, entity => entity == id);
        }
        // public async Task DeleteAsync(string id) =>
        //     await Collection.DeleteOneAsync(_clientSessionHandle, f => f.Id == id);
    }
}
