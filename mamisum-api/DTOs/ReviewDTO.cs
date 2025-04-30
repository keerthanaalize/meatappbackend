namespace mamisum_api.DTOs
{
    public class ReviewDTO
    {
        public string? Id { get; set; }
        public string ReviewTitle { get; set; } = string.Empty;
        public string ReviewDescription { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string ReviewImage { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
    }
}
