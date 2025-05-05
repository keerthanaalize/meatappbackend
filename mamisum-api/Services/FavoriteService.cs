using mamisum_api.Models.Users;
using mamisum_api.Repositories;

namespace mamisum_api.Services
{
    public class FavoriteService
    {
        private readonly IFavoriteRepository _repo;

        public FavoriteService(IFavoriteRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Favorite>> GetUserFavoritesAsync(string userId)
        {
            return await _repo.GetByUserIdAsync(userId);
        }

        public async Task<bool> ToggleFavoriteAsync(string userId, string? productId, string? shopId)
        {
            var existing = await _repo.GetFavoriteAsync(userId, productId, shopId);

            if (existing != null)
            {
                return await _repo.DeleteAsync(existing.Id);
            }

            var favorite = new Favorite
            {
                UserId = userId,
                ProductId = productId,
                ShopId = shopId
            };

            await _repo.CreateAsync(favorite);
            return true;
        }

        public async Task<bool> RemoveFavoriteAsync(string userId, string? productId, string? shopId)
        {
            return await _repo.RemoveFavoriteAsync(userId, productId, shopId);
        }
    }
}
