using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace mamisum_api.Models
{
    public class Deal
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("Title")]
        public string Title { get; set; } = string.Empty;

        [BsonElement("ImageUrl")]
        public string ImageUrl { get; set; } = string.Empty;

        [BsonElement("UserId")]
        public string UserId { get; set; } = string.Empty;
    }
}
