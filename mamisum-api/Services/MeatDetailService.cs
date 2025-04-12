using mamisum_api.Models;
using mamisum_api.Repositories;

namespace mamisum_api.Services
{
    public class MeatDetailService
    {
        private readonly IMeatDetailRepository _meatRepo;

        public MeatDetailService(IMeatDetailRepository meatRepo)
        {
            _meatRepo = meatRepo;
        }

        public async Task<List<MeatDetail>> GetMeatsByCategoryAsync(string categoryId) =>
            await _meatRepo.GetMeatsByCategoryAsync(categoryId);

        public async Task<MeatDetail?> GetMeatByIdAsync(string id) =>
            await _meatRepo.GetMeatByIdAsync(id);

        public async Task<bool> AddMeatAsync(MeatDetail meat)
        {
            await _meatRepo.CreateMeatAsync(meat);
            return true;
        }

        public async Task<bool> UpdateMeatAsync(MeatDetail meat) =>
            await _meatRepo.UpdateMeatAsync(meat);

        public async Task<bool> DeleteMeatAsync(string id) =>
            await _meatRepo.DeleteMeatAsync(id);

        public async Task<List<MeatDetail>> GetMeatsByCategoryAsync(string categoryId, string userId) =>
            await _meatRepo.GetMeatsByCategoryAsync(categoryId, userId);

        public async Task<List<MeatDetail>> GetMeatsByUserIdAsync(string userId)
        {
            return await _meatRepo.GetMeatsByUserIdAsync(userId);
        }

    }
}
