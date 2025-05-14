using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace mamisum_api.Models
{
    public class Cart
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        [BsonElement("ProductId")]
        public string ProductId { get; set; } = string.Empty;

        [BsonElement("ProductName")]
        public string ProductName { get; set; } = string.Empty;

        [BsonElement("Quantity")]
        public int Quantity { get; set; }

        [BsonElement("Price")]
        public decimal Price { get; set; }

        [BsonElement("Discount")]
        public decimal Discount { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; } = string.Empty;
        [BsonElement("isFavorite")]
        public bool isFavorite { get; set; } = false;
        [BsonElement("Rating")]
        public string Rating { get; set; } = string.Empty;
        [BsonElement("VoiceMessageUrl")]
        public string VoiceMessageUrl { get; set; } = string.Empty;
        [BsonElement("UserId")]
        public string UserId { get; set; } = string.Empty;
    }
}
