using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mamisum_api.Models
{
    public class Review
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("ReviewTitle")]
        public string ReviewTitle { get; set; } = string.Empty;

        [BsonElement("ReviewDescription")]
        public string ReviewDescription { get; set; } = string.Empty;

        [BsonElement("Rating")]
        public int Rating { get; set; }

        [BsonElement("ReviewImage")]
        public string ReviewImage { get; set; } = string.Empty;

        [BsonElement("UserId")]
        public string UserId { get; set; } = string.Empty;

        [BsonElement("ProductId")]
        public string ProductId { get; set; } = string.Empty;
    }
}
