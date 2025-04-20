using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace mamisum_api.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("OrderNo")]
        public string OrderNo { get; set; } = string.Empty;

        [BsonElement("ProductId")]
        public string ProductId { get; set; } = string.Empty;

        [BsonElement("ItemName")]
        public string ItemName { get; set; } = string.Empty;

        [BsonElement("Quantity")]
        public int Quantity { get; set; }

        [BsonElement("Price")]
        public string Price { get; set; } = string.Empty;

        [BsonElement("SGST")]
        public string SGST { get; set; } = string.Empty;

        [BsonElement("CGST")]
        public string CGST { get; set; } = string.Empty;

        [BsonElement("TotalBillAmount")]
        public string TotalBillAmount { get; set; } = string.Empty;

        [BsonElement("OrderStatus")]
        public string OrderStatus { get; set; } = "InProgress";
        [BsonElement("CustomerId")]
        public string CustomerId { get; set; } = string.Empty;

    }
}
