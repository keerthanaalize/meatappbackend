using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace mamisum_api.Models
{
    public class ShopProfile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string ShopImage { get; set; } = string.Empty;
        public string ShopName { get; set; } = string.Empty;
        public string DocumentImage { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public string ShopAddress { get; set; } = string.Empty;
        public string ShopCity { get; set; } = string.Empty;
        public string ShopState { get; set; } = string.Empty;
        public string ShopCategory { get; set; } = string.Empty;
        public string OwnerName { get; set; } = string.Empty;
        public string ShopOpenTime { get; set; } = string.Empty;
        public string ShopCloseTime { get; set; } = string.Empty;
        public string ShopDate { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}


 
    
