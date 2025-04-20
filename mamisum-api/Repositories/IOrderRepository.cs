using mamisum_api.Models;
using mamisum_api.Models.Users;

namespace mamisum_api.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(string id);
        Task CreateAsync(Order order);
        Task<bool> UpdateAsync(Order order);
        Task<bool> DeleteAsync(string id);
        Task<List<Order>> GetOrdersByShopperAsync(string shopperId);
        Task<Order?> GetOrderByIdAsync(string id);
        Task<List<Order>> GetOrdersByCustomerIdAsync(string customerId);
    }
}
