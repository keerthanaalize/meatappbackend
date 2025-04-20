using mamisum_api.Models;
using mamisum_api.Repositories;

namespace mamisum_api.Services
{
    public class CartService
    {
        private readonly ICartRepository _cartRepo;

        public CartService(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
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
