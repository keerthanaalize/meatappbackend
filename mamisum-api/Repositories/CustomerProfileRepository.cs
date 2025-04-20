using mamisum_api.Models;
using mamisum_api.Models.Users;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace mamisum_api.Repositories
{
    public class CustomerProfileRepository : ICustomerProfileRepository
    {
        private readonly IMongoCollection<CustomerProfile> _profiles;

        public CustomerProfileRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _profiles = database.GetCollection<CustomerProfile>("CustomerProfiles");
        }

        public async Task<CustomerProfile?> GetByUserIdAsync(string userId) =>
            await _profiles.Find(x => x.UserId == userId).FirstOrDefaultAsync();

        public async Task CreateAsync(CustomerProfile profile) =>
            await _profiles.InsertOneAsync(profile);

        public async Task<bool> UpdateAsync(CustomerProfile profile)
        {
            var result = await _profiles.ReplaceOneAsync(x => x.UserId == profile.UserId, profile);
            return result.ModifiedCount > 0;
        }
    }
}
