using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace mamisum_api.Models
{
    public class TotalOrder
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string OrderNo { get; set; } = string.Empty;
        public decimal TotalBillAmount { get; set; }
        public string DeliveryStatus { get; set; } = string.Empty;
    }
}
