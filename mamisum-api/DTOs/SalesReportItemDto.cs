namespace mamisum_api.DTOs
{
    public class SalesReportItemDto
    {
        public string OrderNo { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; } 
        public List<SalesProductDto> Products { get; set; } = new();
        public decimal SGST { get; set; }
        public decimal CGST { get; set; }
        public decimal TotalBillAmount { get; set; }
    }
}
