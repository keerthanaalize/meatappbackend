using mamisum_api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace mamisum_api.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IMongoCollection<Review> _reviews;

        public ReviewRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var db = client.GetDatabase(settings.Value.DatabaseName);
            _reviews = db.GetCollection<Review>("Reviews");
        }

        public async Task<List<Review>> GetAllAsync() => await _reviews.Find(_ => true).ToListAsync();

        public async Task<Review?> GetByIdAsync(string id) =>
            await _reviews.Find(r => r.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Review review) => await _reviews.InsertOneAsync(review);

        public async Task<bool> UpdateAsync(Review review)
        {
            var result = await _reviews.ReplaceOneAsync(r => r.Id == review.Id, review);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _reviews.DeleteOneAsync(r => r.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<List<Review>> GetByProductIdAsync(string productId) =>
            await _reviews.Find(r => r.ProductId == productId).ToListAsync();

        public async Task<List<Review>> GetByUserIdAsync(string userId) =>
            await _reviews.Find(r => r.UserId == userId).ToListAsync();
    }
}

