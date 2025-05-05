using mamisum_api.Models.Users;

namespace mamisum_api.Repositories
{
    public interface IFavoriteRepository
    {
         Task<List<Favorite>> GetByUserIdAsync(string userId);
        Task<Favorite?> GetFavoriteAsync(string userId, string? productId, string? shopId);
        Task CreateAsync(Favorite favorite);
        Task<bool> DeleteAsync(string id);
        Task<bool> RemoveFavoriteAsync(string userId, string? productId, string? shopId);
        Task<bool> DeleteByDetailsAsync(string userId, string? productId, string? shopId);
    }
}
