using mamisum_api.Models;

namespace mamisum_api.Repositories
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllAsync();
        Task<Review?> GetByIdAsync(string id);
        Task CreateAsync(Review review);
        Task<bool> UpdateAsync(Review review);
        Task<bool> DeleteAsync(string id);
        Task<List<Review>> GetByProductIdAsync(string productId);
        Task<List<Review>> GetByUserIdAsync(string userId);
    }
}
