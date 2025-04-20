using mamisum_api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace mamisum_api.Repositories
{
    public class TotalOrderRepository : ITotalOrderRepository
    {
        private readonly IMongoCollection<TotalOrder> _collection;

        public TotalOrderRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var db = client.GetDatabase(settings.Value.DatabaseName);
            _collection = db.GetCollection<TotalOrder>("TotalOrders");
        }

        public async Task<List<TotalOrder>> GetAllAsync() => await _collection.Find(_ => true).ToListAsync();

        public async Task<TotalOrder?> GetByIdAsync(string id) =>
            await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(TotalOrder order) =>
            await _collection.InsertOneAsync(order);

        public async Task<bool> UpdateAsync(TotalOrder order)
        {
            var result = await _collection.ReplaceOneAsync(x => x.Id == order.Id, order);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _collection.DeleteOneAsync(x => x.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
