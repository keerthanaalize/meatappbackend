using mamisum_api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace mamisum_api.Repositories
{
    public class MeatDetailRepository : IMeatDetailRepository
    {
        private readonly IMongoCollection<MeatDetail> _meats;

        public MeatDetailRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _meats = database.GetCollection<MeatDetail>("MeatDetails");
        }

        public async Task<List<MeatDetail>> GetMeatsByCategoryAsync(string categoryId) =>
            await _meats.Find(m => m.CategoryId == categoryId).ToListAsync();

        public async Task<MeatDetail?> GetMeatByIdAsync(string id) =>
            await _meats.Find(m => m.Id == id).FirstOrDefaultAsync();

        public async Task CreateMeatAsync(MeatDetail meat) =>
            await _meats.InsertOneAsync(meat);

        public async Task<bool> UpdateMeatAsync(MeatDetail meat)
        {
            var result = await _meats.ReplaceOneAsync(m => m.Id == meat.Id, meat);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteMeatAsync(string id)
        {
            var result = await _meats.DeleteOneAsync(m => m.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<List<MeatDetail>> GetMeatsByUserIdAsync(string userId)
        {
            return await _meats.Find(m => m.UserId == userId).ToListAsync();
        }


        public async Task<List<MeatDetail>> GetMeatsByCategoryAsync(string categoryId, string userId)
        {
            return await _meats.Find(m => m.CategoryId == categoryId && m.UserId == userId).ToListAsync();
        }

    }
}
