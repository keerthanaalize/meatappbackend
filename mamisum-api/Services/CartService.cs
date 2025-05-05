using mamisum_api.DTOs;
using mamisum_api.Models;
using mamisum_api.Repositories;

namespace mamisum_api.Services
{
    public class CartService
    {
        private readonly ICartRepository _cartRepo;
        private readonly IFavoriteRepository _favoriteRepo;

        public CartService(ICartRepository cartRepo, IFavoriteRepository favoriteRepo)
        {
            _cartRepo = cartRepo;
            _favoriteRepo = favoriteRepo;
        }

        public async Task<List<CartWithFavoriteDto>> GetCartWithFavoriteAsync(string userId)
        {
            var carts = await _cartRepo.GetCartsByUserIdAsync(userId);
            var favorites = await _favoriteRepo.GetByUserIdAsync(userId);
            var favoriteProductIds = favorites
                .Where(f => !string.IsNullOrEmpty(f.ProductId))
                .Select(f => f.ProductId)
                .ToHashSet();

            return carts.Select(cart => new CartWithFavoriteDto
            {
                Id = cart.Id,
                ProductId = cart.ProductId,
                ProductName = cart.ProductName,
                Quantity = cart.Quantity,
                Price = cart.Price,
                Discount = cart.Discount,
                Description = cart.Description,
                UserId = cart.UserId,
                IsFavorite = favoriteProductIds.Contains(cart.ProductId) ? "Yes" : "No"
            }).ToList();
        }
        public async Task<List<Cart>> GetCartsAsync(string userId) =>
            await _cartRepo.GetCartsByUserIdAsync(userId);

        public async Task<Cart?> GetCartByIdAsync(string id) =>
            await _cartRepo.GetCartByIdAsync(id);

        public async Task AddToCartAsync(Cart cart) =>
            await _cartRepo.AddToCartAsync(cart);

        public async Task<bool> UpdateCartAsync(Cart cart) =>
            await _cartRepo.UpdateCartAsync(cart);

        public async Task<bool> RemoveFromCartAsync(string id) =>
            await _cartRepo.RemoveFromCartAsync(id);
    }
}
