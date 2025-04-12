using mamisum_api.Models;
using mamisum_api.Models.Users;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace mamisum_api.Services
{
    public class ShopperDetailsService
    {
        private readonly IMongoCollection<ShopperDetails> _shopperDetails;

        public ShopperDetailsService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _shopperDetails = database.GetCollection<ShopperDetails>("ShopperDetails");
        }

        public async Task<bool> UpsertShopperDetails(string shopperId, ShopperDetails request)
        {
            var existingDetails = await _shopperDetails.Find(cd => cd.ShopperId == shopperId).FirstOrDefaultAsync();

            if (existingDetails == null)
            {
                request.ShopperId = shopperId;
                await _shopperDetails.InsertOneAsync(request);
            }
            else
            {
                var update = Builders<ShopperDetails>.Update
                    .Set(cd => cd.Address, request.Address)
                    .Set(cd => cd.PhoneNumber, request.PhoneNumber)
                    .Set(cd => cd.ShopName, request.ShopName)
                    .Set(cd => cd.City, request.City)
                    .Set(cd => cd.PinCode, request.PinCode);

                var result = await _shopperDetails.UpdateOneAsync(cd => cd.ShopperId == shopperId, update);

                return result.ModifiedCount > 0;
            }

            return true;
        }

        public async Task<ShopperDetails?> GetShopperDetailsByUserId(string shopperId)
        {
            return await _shopperDetails.Find(cd => cd.ShopperId == shopperId).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteShopperDetails(string shopperId)
        {
            var result = await _shopperDetails.DeleteOneAsync(cd => cd.ShopperId == shopperId);
            return result.DeletedCount > 0;
        }
    }
}
