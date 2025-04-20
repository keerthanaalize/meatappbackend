using mamisum_api.Models;

namespace mamisum_api.Repositories
{
    public interface IShopProfileRepository
    {
        Task<List<ShopProfile>> GetAllAsync();
        Task<ShopProfile?> GetByIdAsync(string id);
        Task CreateAsync(ShopProfile profile);
        Task<bool> UpdateAsync(string id, ShopProfile profile);
    }
}
