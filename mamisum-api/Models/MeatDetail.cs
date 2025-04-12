using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mamisum_api.Models
{
    public class MeatDetail
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("Name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("PricePerKg")]
        public decimal PricePerKg { get; set; }

        [BsonElement("CategoryId")]
        public string CategoryId { get; set; } = string.Empty;

        [BsonElement("ImageUrl")]
        public string? ImageUrl { get; set; }

        [BsonElement("Description")]
        public string? Description { get; set; }
        [BsonElement("UserId")]
        public string UserId { get; set; } = string.Empty;
    }
}
