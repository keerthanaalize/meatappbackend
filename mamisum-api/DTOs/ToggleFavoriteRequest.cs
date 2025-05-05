namespace mamisum_api.DTOs
{
    public class ToggleFavoriteRequest
    {
        public string UserId { get; set; } = string.Empty;
        public string? ProductId { get; set; }
        public string? ShopId { get; set; }
    }
}
