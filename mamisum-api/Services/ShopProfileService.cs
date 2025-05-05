using mamisum_api.Models;
using mamisum_api.Models.Users;
using mamisum_api.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace mamisum_api.Services
{
    public class ShopProfileService
    {
        private readonly IShopProfileRepository _repo;

        public ShopProfileService(IShopProfileRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ShopProfile>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<ShopProfile?> GetByUserIdAsync(string userId)  => await _repo.GetByUserIdAsync(userId);
        public async Task<ShopProfile?> GetByIdAsync(string id) => await _repo.GetByIdAsync(id);
        public async Task CreateAsync(ShopProfile profile) => await _repo.CreateAsync(profile);
        public async Task<List<ShopProfile>> GetShopsByCityAsync(string city)
        {
            return await _repo.GetByCityAsync(city);
        }


        public async Task<List<ShopProfile>> GetNearbyShopsAsync(string userId, ICustomerProfileRepository customerRepo)
        {
            var customer = await customerRepo.GetByUserIdAsync(userId);
            if (customer == null || string.IsNullOrWhiteSpace(customer.City))
                return new List<ShopProfile>();

            return await _repo.GetByCityAsync(customer.City); 
        }

        public async Task<List<ShopProfile>> GetNearbyShopsByLocationAsync(string userId, ICustomerProfileRepository customerRepo, double distanceKm = 5)
        {
            var customer = await customerRepo.GetByUserIdAsync(userId);
            if (customer == null || customer.Location == null || customer.Location.Length != 2)
                return new List<ShopProfile>();

            return await _repo.GetNearbyShopsByLocationAsync(
                customer.Location[0], 
                customer.Location[1], 
                distanceKm
            );
        }

        public async Task<List<ShopProfile>> GetByCategoryAsync(string category) => await _repo.GetByCategoryAsync(category);
        public async Task<bool> UpdateAsync(string id, ShopProfile profile) => await _repo.UpdateAsync(id, profile);
    }
}
