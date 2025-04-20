namespace mamisum_api.DTOs
{
    public class CreateShopProfileDto
    {
        public string ShopName { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public string ShopAddress { get; set; } = string.Empty;
        public string OwnerName { get; set; } = string.Empty;
        public string ShopOpenTime { get; set; } = string.Empty;
        public string ShopCloseTime { get; set; } = string.Empty;
        public IFormFile? ShopImage { get; set; }
        public IFormFile? DocumentImage { get; set; }
    }
}
