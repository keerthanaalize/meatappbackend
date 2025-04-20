namespace mamisum_api.DTOs
{
    public class UpdateTotalOrderDto
    {
        public decimal TotalBillAmount { get; set; }
        public string DeliveryStatus { get; set; } = string.Empty;
    }
}
