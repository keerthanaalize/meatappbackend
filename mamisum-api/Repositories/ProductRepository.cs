using mamisum_api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace mamisum_api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _products = database.GetCollection<Product>("Products");
        }

        public async Task<List<Product>> GetProductsAsync(string userId)
        {
            return await _products.Find(p => p.UserId == userId).ToListAsync();
        }

        public async Task<List<Product>> GetProductsByShopIdAsync(string shopId)
        {
            return await _products.Find(p => p.ShopId == shopId).ToListAsync();
        }

        public async Task<Product?> GetMeatByIdAsync(string id) =>
            await _products.Find(m => m.Id == id).FirstOrDefaultAsync();

        public async Task CreateMeatAsync(Product meat) =>
            await _products.InsertOneAsync(meat);

        public async Task<bool> UpdateMeatAsync(Product meat)
        {
            var result = await _products.ReplaceOneAsync(m => m.Id == meat.Id, meat);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteMeatAsync(string id)
        {
            var result = await _products.DeleteOneAsync(m => m.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<List<Product>> GetMeatsByUserIdAsync(string userId)
        {
            return await _products.Find(m => m.UserId == userId).ToListAsync();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _products.Find(_ => true).ToListAsync();
        }
    }
}
