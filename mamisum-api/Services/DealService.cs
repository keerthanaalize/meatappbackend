using mamisum_api.Models;
using mamisum_api.Repositories;

namespace mamisum_api.Services
{
    public class DealService
    {
        private readonly IDealRepository _repo;

        public DealService(IDealRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Deal>> GetAllDealsAsync() =>
            await _repo.GetAllDealsAsync();

        public async Task<Deal?> GetDealByIdAsync(string id) =>
            await _repo.GetDealByIdAsync(id);

        public async Task<bool> AddDealAsync(Deal deal)
        {
            await _repo.CreateDealAsync(deal);
            return true;
        }

        public async Task<List<Deal>> GetDealsByUserIdAsync(string userId)
        {
            return await _repo.GetDealsByUserIdAsync(userId);
        }
        public async Task<bool> DeleteDealAsync(string id) =>
            await _repo.DeleteDealAsync(id);
    }
}
