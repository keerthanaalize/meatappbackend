using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace mamisum_api.Models
{
    public class ShopperDetails
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("shopperId")]
        public string ShopperId { get; set; }

        [BsonElement("shopName")]
        public string ShopName { get; set; }

        [BsonElement("phoneNumber")]
        public string PhoneNumber { get; set; }

        [BsonElement("city")]
        public string City { get; set; }

        [BsonElement("pinCode")]
        public string PinCode { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }
    }
}


 
    
