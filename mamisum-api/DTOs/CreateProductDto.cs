namespace mamisum_api.DTOs
{
    public class CreateProductDto
    {
        public string ProductName { get; set; } = string.Empty;
        public string ProductCategoryId { get; set; } = string.Empty;
        public string ProductCategoryName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public string ProductQuantity { get; set; } = string.Empty;
        public string ProductPrice { get; set; } = string.Empty;
        public string Discount { get; set; } = string.Empty;
        public string KeyFeatures { get; set; } = string.Empty;
        public string ProductAvailability { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;
        public string Rating { get; set; } = string.Empty;
        public bool Favourite { get; set; } = false;
        public IFormFile? ImageFile { get; set; }
    }

}
