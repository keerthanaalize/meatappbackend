using mamisum_api.Models;

namespace mamisum_api.Repositories
{
    public interface ITotalOrderRepository
    {
        Task<List<TotalOrder>> GetAllAsync();
        Task<TotalOrder?> GetByIdAsync(string id);
        Task CreateAsync(TotalOrder order);
        Task<bool> UpdateAsync(TotalOrder order);
        Task<bool> DeleteAsync(string id);
    }
}
