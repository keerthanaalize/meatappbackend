using mamisum_api.Models;
using mamisum_api.Repositories;

namespace mamisum_api.Services
{
    public class DealService
    {
        private readonly IDealRepository _dealRepository;

        public DealService(IDealRepository dealRepository)
        {
            _dealRepository = dealRepository;
        }

        public async Task<List<Deal>> GetDealsByUserIdAsync(string userId)
        {
            return await _dealRepository.GetDealsByUserIdAsync(userId);
        }

        public async Task<Deal?> GetDealByIdAsync(string id)
        {
            return await _dealRepository.GetDealByIdAsync(id);
        }

        public async Task CreateDealAsync(Deal deal)
        {
            await _dealRepository.CreateDealAsync(deal);
        }

        public async Task<bool> DeleteDealAsync(string id)
        {
            return await _dealRepository.DeleteDealAsync(id);
        }
    }
}
