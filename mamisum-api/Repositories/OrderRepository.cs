using mamisum_api.Models;
using mamisum_api.Models.Users;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace mamisum_api.Repositories
{
    public class OrderRepository   : IOrderRepository
    {
        private readonly IMongoCollection<Order> _orders;
        private readonly IMongoCollection<Product> _products;
        private readonly IMongoCollection<CustomerProfile> _customerProfiles;
        public OrderRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var db = client.GetDatabase(settings.Value.DatabaseName);
            _orders = db.GetCollection<Order>("Orders");
            _products = db.GetCollection<Product>("Products");
            _customerProfiles = db.GetCollection<CustomerProfile>("CustomerProfiles");
        }

        public async Task<List<Order>> GetAllAsync() =>
            await _orders.Find(_ => true).ToListAsync();


        public async Task<Order?> GetOrderByIdAsync(string id)
        {
            return await _orders.Find(o => o.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Order>> GetOrdersByCustomerIdAsync(string customerId)
        {
            return await _orders.Find(o => o.CustomerId == customerId).ToListAsync();
        }


        public async Task<Order?> GetByIdAsync(string id) =>
            await _orders.Find(o => o.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Order order) =>
            await _orders.InsertOneAsync(order);

        public async Task<bool> UpdateAsync(Order order)
        {
            var result = await _orders.ReplaceOneAsync(o => o.Id == order.Id, order);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _orders.DeleteOneAsync(o => o.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<List<Order>> GetOrdersByShopperAsync(string shopperId)
        {
            var productIds = await _products
                .Find(p => p.UserId == shopperId)
                .Project(p => p.Id)
                .ToListAsync();

            var filter = Builders<Order>.Filter.ElemMatch(o => o.Items, i => productIds.Contains(i.ProductId));
            var orders = await _orders.Find(filter).ToListAsync();

            return orders;
        }
    }
}
