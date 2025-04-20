using mamisum_api.Models;

namespace mamisum_api.Repositories
{
    public interface IDealRepository
    {
        Task<List<Deal>> GetAllDealsAsync();
        Task<Deal?> GetDealByIdAsync(string id);
        Task CreateDealAsync(Deal deal);
        Task<bool> DeleteDealAsync(string id);
        Task<List<Deal>> GetDealsByUserIdAsync(string userId);
    }
}
