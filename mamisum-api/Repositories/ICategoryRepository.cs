using mamisum_api.Models;

namespace mamisum_api.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(string id);
        Task CreateCategoryAsync(Category category);
        Task<bool> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(string id);
        Task<List<Category>> GetCategoriesByUserIdAsync(string userId);
    }
}
