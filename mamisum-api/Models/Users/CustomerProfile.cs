using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace mamisum_api.Models.Users
{
    public class CustomerProfile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ProfileImage { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}
