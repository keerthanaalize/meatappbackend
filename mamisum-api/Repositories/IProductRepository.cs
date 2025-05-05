using mamisum_api.Models;

namespace mamisum_api.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync(string userId);
        Task<Product?> GetMeatByIdAsync(string id);
        Task CreateMeatAsync(Product meat);
        Task<bool> UpdateMeatAsync(Product meat);
        Task<bool> DeleteMeatAsync(string id);
        Task<List<Product>> GetMeatsByUserIdAsync(string userId);
        Task<List<Product>> GetProductsByShopIdAsync(string shopId);
        Task<List<Product>> GetAllProductsAsync();

    }
}
