using mamisum_api.Models;
using mamisum_api.Models.Users;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace mamisum_api.Services
{
    public class CustomerDetailsService
    {
        private readonly IMongoCollection<CustomerDetails> _customerDetails;

        public CustomerDetailsService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _customerDetails = database.GetCollection<CustomerDetails>("CustomerDetails");
        }

        public async Task<bool> UpsertCustomerDetails(string userId, CustomerDetails request)
        {
            var existingDetails = await _customerDetails.Find(cd => cd.UserId == userId).FirstOrDefaultAsync();

            if (existingDetails == null)
            {
                request.UserId = userId;
                await _customerDetails.InsertOneAsync(request);
            }
            else
            {
                var update = Builders<CustomerDetails>.Update
                    .Set(cd => cd.Address, request.Address)
                    .Set(cd => cd.PhoneNumber, request.PhoneNumber);

                var result = await _customerDetails.UpdateOneAsync(cd => cd.UserId == userId, update);

                return result.ModifiedCount > 0;
            }

            return true;
        }

        public async Task<CustomerDetails?> GetCustomerDetailsByUserId(string userId)
        {
            return await _customerDetails.Find(cd => cd.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteCustomerDetails(string userId)
        {
            var result = await _customerDetails.DeleteOneAsync(cd => cd.UserId == userId);
            return result.DeletedCount > 0;
        }
    }
}
