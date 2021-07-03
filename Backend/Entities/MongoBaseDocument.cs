using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Virta.Entities
{
    public class MongoBaseDocument
    {
        [BsonId]
        public ObjectId? Id { get; set; }
    }
}
