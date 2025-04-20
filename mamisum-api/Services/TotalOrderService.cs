using mamisum_api.Models;
using mamisum_api.Repositories;

namespace mamisum_api.Services
{
    public class TotalOrderService
    {
        private readonly ITotalOrderRepository _repo;

        public TotalOrderService(ITotalOrderRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<TotalOrder>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<TotalOrder?> GetByIdAsync(string id) => await _repo.GetByIdAsync(id);

        public async Task CreateAsync(TotalOrder order) => await _repo.CreateAsync(order);

        public async Task<bool> UpdateAsync(TotalOrder order) => await _repo.UpdateAsync(order);

        public async Task<bool> DeleteAsync(string id) => await _repo.DeleteAsync(id);
    }
}
