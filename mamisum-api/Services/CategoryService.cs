using mamisum_api.Models;
using mamisum_api.Repositories;

namespace mamisum_api.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> GetAllCategoriesAsync() =>
            await _categoryRepository.GetCategoriesAsync();

        public async Task<Category?> GetCategoryByIdAsync(string id) =>
            await _categoryRepository.GetCategoryByIdAsync(id);

        public async Task<bool> AddCategoryAsync(Category category)
        {
            await _categoryRepository.CreateCategoryAsync(category);
            return true;
        }

        public async Task<bool> UpdateCategoryAsync(Category category) =>
            await _categoryRepository.UpdateCategoryAsync(category);

        public async Task<bool> DeleteCategoryAsync(string id) =>
            await _categoryRepository.DeleteCategoryAsync(id);

        public async Task<List<Category>> GetCategoriesByUserIdAsync(string userId)
        {
            return await _categoryRepository.GetCategoriesByUserIdAsync(userId);
        }
    }
}
