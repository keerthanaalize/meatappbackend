using mamisum_api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace mamisum_api.Repositories
{
    public class DealRepository : IDealRepository
    {
        private readonly IMongoCollection<Deal> _deals;

        public DealRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _deals = database.GetCollection<Deal>("Deals");
        }

        public async Task<List<Deal>> GetAllDealsAsync() =>
            await _deals.Find(_ => true).ToListAsync();

        public async Task<Deal?> GetDealByIdAsync(string id) =>
            await _deals.Find(d => d.Id == id).FirstOrDefaultAsync();

        public async Task CreateDealAsync(Deal deal) =>
            await _deals.InsertOneAsync(deal);

        public async Task<bool> DeleteDealAsync(string id)
        {
            var result = await _deals.DeleteOneAsync(d => d.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<List<Deal>> GetDealsByUserIdAsync(string userId)
        {
            return await _deals.Find(d => d.UserId == userId).ToListAsync();
        }

    }
}
