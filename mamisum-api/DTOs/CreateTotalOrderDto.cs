namespace mamisum_api.DTOs
{
    public class CreateTotalOrderDto
    {
        public string OrderNo { get; set; } = string.Empty;
        public decimal TotalBillAmount { get; set; }
        public string DeliveryStatus { get; set; } = string.Empty;
    }
}
