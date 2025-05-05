using mamisum_api.Models;
using mamisum_api.Models.Users;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace mamisum_api.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly IMongoCollection<Favorite> _favorites;

        public FavoriteRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var db = client.GetDatabase(settings.Value.DatabaseName);
            _favorites = db.GetCollection<Favorite>("Favorites");
        }

        public async Task<List<Favorite>> GetByUserIdAsync(string userId)
        {
            return await _favorites.Find(f => f.UserId == userId).ToListAsync();
        }

        public async Task<Favorite?> GetFavoriteAsync(string userId, string? productId, string? shopId)
        {
            var filter = Builders<Favorite>.Filter.Eq(f => f.UserId, userId);

            if (!string.IsNullOrEmpty(productId))
                filter &= Builders<Favorite>.Filter.Eq(f => f.ProductId, productId);
            if (!string.IsNullOrEmpty(shopId))
                filter &= Builders<Favorite>.Filter.Eq(f => f.ShopId, shopId);

            return await _favorites.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Favorite favorite)
        {
            await _favorites.InsertOneAsync(favorite);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _favorites.DeleteOneAsync(f => f.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<bool> RemoveFavoriteAsync(string userId, string? productId, string? shopId)
        {
            var builder = Builders<Favorite>.Filter;
            var filter = builder.Eq(f => f.UserId, userId);

            if (!string.IsNullOrEmpty(productId))
                filter &= builder.Eq(f => f.ProductId, productId);

            if (!string.IsNullOrEmpty(shopId))
                filter &= builder.Eq(f => f.ShopId, shopId);

            var result = await _favorites.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }


        public async Task<bool> DeleteByDetailsAsync(string userId, string? productId, string? shopId)
        {
            var filter = Builders<Favorite>.Filter.Eq(f => f.UserId, userId);

            if (!string.IsNullOrEmpty(productId))
                filter &= Builders<Favorite>.Filter.Eq(f => f.ProductId, productId);
            if (!string.IsNullOrEmpty(shopId))
                filter &= Builders<Favorite>.Filter.Eq(f => f.ShopId, shopId);

            var result = await _favorites.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }
    }
}
