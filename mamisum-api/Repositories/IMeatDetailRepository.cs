using mamisum_api.Models;

namespace mamisum_api.Repositories
{
    public interface IMeatDetailRepository
    {
        Task<List<MeatDetail>> GetMeatsByCategoryAsync(string categoryId);
        Task<MeatDetail?> GetMeatByIdAsync(string id);
        Task CreateMeatAsync(MeatDetail meat);
        Task<bool> UpdateMeatAsync(MeatDetail meat);
        Task<bool> DeleteMeatAsync(string id);
        Task<List<MeatDetail>> GetMeatsByCategoryAsync(string categoryId, string userId);
        Task<List<MeatDetail>> GetMeatsByUserIdAsync(string userId);


    }
}
