using mamisum_api.Models;

namespace mamisum_api.Repositories
{
    public interface IShopProfileRepository
    {
        Task<List<ShopProfile>> GetAllAsync();
        Task<ShopProfile?> GetByUserIdAsync(string userId);
        Task<ShopProfile?> GetByIdAsync(string id);
        Task CreateAsync(ShopProfile profile);
        Task<bool> UpdateAsync(string id, ShopProfile profile);
        Task<List<ShopProfile>> GetNearbyShopsByLocationAsync(double longitude, double latitude, double maxDistanceInKm);
        Task<List<ShopProfile>> GetByCityAsync(string city);
        Task<List<ShopProfile>> GetByUserAllIdAsync(string userId);
        Task<List<ShopProfile>> GetByCategoryAsync(string category);
    }
}
