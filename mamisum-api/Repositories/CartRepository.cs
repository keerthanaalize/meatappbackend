


using mamisum_api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace mamisum_api.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IMongoCollection<Cart> _carts;

        public CartRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _carts = database.GetCollection<Cart>("Carts");
        }

        public async Task<List<Cart>> GetCartsByUserIdAsync(string userId) =>
            await _carts.Find(c => c.UserId == userId).ToListAsync();

        public async Task<Cart?> GetCartByIdAsync(string id) =>
            await _carts.Find(c => c.Id == id).FirstOrDefaultAsync();

        public async Task AddToCartAsync(Cart cart) =>
            await _carts.InsertOneAsync(cart);

        public async Task<bool> UpdateCartAsync(Cart cart)
        {
            var result = await _carts.ReplaceOneAsync(c => c.Id == cart.Id, cart);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> RemoveFromCartAsync(string id)
        {
            var result = await _carts.DeleteOneAsync(c => c.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<bool> ClearCartAsync(string userId)
        {
            var result = await _carts.DeleteManyAsync(c => c.UserId == userId);
            return result.DeletedCount > 0;
        }
    }
}
