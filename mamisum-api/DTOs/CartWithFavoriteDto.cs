using mamisum_api.Models.Users;

namespace mamisum_api.DTOs
{
    public class CartWithFavoriteDto
    {
        public string Id { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string Description { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Rating { get; set; } = string.Empty;
        public string VoiceMessageUrl { get; set; } = string.Empty;

        public bool IsFavorite => Favorite != null;
        public Favorite? Favorite { get; set; }
    }
}
