using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace mamisum_api.Models.Users
{
    public class Favorite
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("UserId")]
        public string UserId { get; set; } = string.Empty;

        [BsonElement("ProductId")]
        public string? ProductId { get; set; }

        [BsonElement("ShopId")]
        public string? ShopId { get; set; }

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
