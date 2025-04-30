using mamisum_api.Models;
using mamisum_api.Repositories;

namespace mamisum_api.Services
{
    public class ProductService
    {
        private readonly IProductRepository _meatRepo;
        
        public ProductService(IProductRepository meatRepo)
        {
            _meatRepo = meatRepo;
        }

        public async Task<Product?> GetMeatByIdAsync(string id) =>
            await _meatRepo.GetMeatByIdAsync(id);

        public async Task<bool> AddMeatAsync(Product meat)
        {
            await _meatRepo.CreateMeatAsync(meat);
            return true;
        }

        public async Task<bool> UpdateMeatAsync(Product meat) =>
            await _meatRepo.UpdateMeatAsync(meat);

        public async Task<bool> DeleteMeatAsync(string id) =>
            await _meatRepo.DeleteMeatAsync(id);

        public async Task<List<Product>> GetMeatsByUserIdAsync(string userId)
        {
            return await _meatRepo.GetMeatsByUserIdAsync(userId);
        }

        public async Task<List<Product>> GetProductsByUserAsync(string userId)
        {
            return await _meatRepo.GetProductsAsync(userId);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _meatRepo.GetAllProductsAsync();
        }

    }
}
