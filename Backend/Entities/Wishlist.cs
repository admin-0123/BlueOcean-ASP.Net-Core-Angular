using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Virta.Entities
{
    public class Wishlist : MongoBaseDocument
    {
        [BsonRepresentation(BsonType.String)]
        public Guid[] ProductIds { get; set; }
    }
}
