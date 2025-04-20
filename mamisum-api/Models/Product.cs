using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mamisum_api.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("ProductName")]
        public string ProductName { get; set; } = string.Empty;
        [BsonElement("ProductCategoryId")]
        public string ProductCategoryId { get; set; } = string.Empty;
        [BsonElement("ProductCategoryName")]
        public string ProductCategoryName { get; set; } = string.Empty;
        [BsonElement("ProductDescription")]
        public string ProductDescription { get; set; } = string.Empty;
        [BsonElement("ProductQuantity")]
        public string ProductQuantity { get; set; } = string.Empty;
        [BsonElement("ProductPrice")]
        public string ProductPrice { get; set; } = string.Empty;
        [BsonElement("Discount")]
        public string Discount { get; set; } = string.Empty;
        [BsonElement("KeyFeatures")]
        public string KeyFeatures { get; set; } = string.Empty;
        [BsonElement("ProductAvailability")]
        public string ProductAvailability { get; set; } = string.Empty;
        [BsonElement("Unit")]
        public string Unit { get; set; } = string.Empty;
        [BsonElement("Tags")]
        public string Tags { get; set; } = string.Empty;
        [BsonElement("Rating")]
        public string Rating { get; set; } = string.Empty;
        [BsonElement("ImageUrl")]
        public string ImageUrl { get; set; } = string.Empty;
        [BsonElement("Favourite")]
        public bool Favourite { get; set; }
        [BsonElement("UserId")]
        public string UserId { get; set; } = string.Empty;
    }
}
