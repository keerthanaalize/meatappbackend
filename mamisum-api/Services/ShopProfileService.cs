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

        public async Task<ShopProfile?> GetByIdAsync(string id) => await _repo.GetByIdAsync(id);

        public async Task CreateAsync(ShopProfile profile) => await _repo.CreateAsync(profile);

        public async Task<bool> UpdateAsync(string id, ShopProfile profile) => await _repo.UpdateAsync(id, profile);
    }
}
