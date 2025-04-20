using mamisum_api.Models;

namespace mamisum_api.Repositories
{
    public interface ICartRepository
    {
        Task<List<Cart>> GetCartsByUserIdAsync(string userId);
        Task<Cart?> GetCartByIdAsync(string id);
        Task AddToCartAsync(Cart cart);
        Task<bool> UpdateCartAsync(Cart cart);
        Task<bool> RemoveFromCartAsync(string id);
    }
}
