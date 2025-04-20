namespace mamisum_api.DTOs
{
    public class OrderWithCustomerDto
    {
        public string OrderNo { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string Price { get; set; } = string.Empty;
        public string TotalBillAmount { get; set; } = string.Empty;
        public string OrderStatus { get; set; } = string.Empty;

        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
    }
}
