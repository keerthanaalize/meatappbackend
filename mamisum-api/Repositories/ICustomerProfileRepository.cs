using mamisum_api.Models.Users;

namespace mamisum_api.Repositories
{
    public interface ICustomerProfileRepository
    {
        Task<CustomerProfile?> GetByUserIdAsync(string userId);
        Task CreateAsync(CustomerProfile profile);
        Task<bool> UpdateAsync(CustomerProfile profile);
    }
}
