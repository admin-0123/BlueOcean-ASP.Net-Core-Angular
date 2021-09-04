using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Virta.Entities
{
    public class Wishlist : MongoBaseDocument
    {
        public IEnumerable<WishlistItem> Products { get; set; }

        public class WishlistItem
        {
            [BsonRepresentation(BsonType.String)]
            public Guid Id { get; set; }
            public string Title { get; set; }
            public decimal Price { get; set; }
            public string[] Images { get; set; }
        }
    }
}
