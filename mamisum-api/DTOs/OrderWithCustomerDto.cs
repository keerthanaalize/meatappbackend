using mamisum_api.Models;

namespace mamisum_api.DTOs
{
    public class OrderWithCustomerDto
    {
        public string OrderNo { get; set; } = string.Empty;
        public List<OrderItem> Items { get; set; } = new();
        public string TotalBillAmount { get; set; } = string.Empty;
        public string OrderStatus { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
