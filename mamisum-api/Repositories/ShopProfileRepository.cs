using mamisum_api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace mamisum_api.Repositories
{
    public class ShopProfileRepository : IShopProfileRepository
    {
        private readonly IMongoCollection<ShopProfile> _collection;

        public ShopProfileRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = database.GetCollection<ShopProfile>("ShopProfiles");
        }

        public async Task<List<ShopProfile>> GetAllAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public async Task<ShopProfile?> GetByIdAsync(string id) =>
            await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ShopProfile profile) =>
            await _collection.InsertOneAsync(profile);

        public async Task<bool> UpdateAsync(string id, ShopProfile profile)
        {
            var result = await _collection.ReplaceOneAsync(p => p.Id == id, profile);
            return result.ModifiedCount > 0;
        }
    }
}
