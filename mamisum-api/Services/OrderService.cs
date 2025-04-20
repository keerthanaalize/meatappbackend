using mamisum_api.Models;
using mamisum_api.Models.Users;
using mamisum_api.Repositories;

namespace mamisum_api.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _repo;
        private readonly ICustomerProfileRepository _customerRepo;
        public OrderService(IOrderRepository repo, ICustomerProfileRepository customerRepo)
        {
            _repo = repo;
            _customerRepo = customerRepo;
        }

        public async Task<List<Order>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Order?> GetByIdAsync(string id) => await _repo.GetByIdAsync(id);

        public async Task CreateAsync(Order order) => await _repo.CreateAsync(order);

        public async Task<bool> UpdateAsync(Order order) => await _repo.UpdateAsync(order);

        public async Task<bool> DeleteAsync(string id) => await _repo.DeleteAsync(id);

        public async Task<List<Order>> GetOrdersByCustomerIdAsync(string customerId)
        {
            return await _repo.GetOrdersByCustomerIdAsync(customerId);
        }

        public async Task<Order?> GetOrderByIdAsync(string id)
        {
            return await _repo.GetOrderByIdAsync(id);
        }

        public async Task<CustomerProfile?> GetCustomerByIdAsync(string customerId)
        {
            return await _customerRepo.GetByUserIdAsync(customerId);
        }
        public async Task<List<Order>> GetOrdersByShopperAsync(string shopperId) =>
            await _repo.GetOrdersByShopperAsync(shopperId);
    }
}
