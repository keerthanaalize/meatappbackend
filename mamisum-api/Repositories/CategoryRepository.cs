using mamisum_api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace mamisum_api.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<Category> _categories;

        public CategoryRepository(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _categories = database.GetCollection<Category>("Categories");
        }

        public async Task<List<Category>> GetCategoriesAsync() =>
            await _categories.Find(category => true).ToListAsync();

        public async Task<Category?> GetCategoryByIdAsync(string id) =>
            await _categories.Find(category => category.Id == id).FirstOrDefaultAsync();

        public async Task CreateCategoryAsync(Category category) =>
            await _categories.InsertOneAsync(category);

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            var result = await _categories.ReplaceOneAsync(c => c.Id == category.Id, category);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteCategoryAsync(string id)
        {
            var result = await _categories.DeleteOneAsync(c => c.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<List<Category>> GetCategoriesByUserIdAsync(string userId)
        {
            return await _categories.Find(c => c.UserId == userId).ToListAsync();
        }
    }
}
