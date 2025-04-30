using mamisum_api.Models;
using mamisum_api.Models.Users;
using mamisum_api.Repositories;

namespace mamisum_api.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _repo;
        private readonly ICustomerProfileRepository _customerRepo;
        private readonly IProductRepository _productRepo;
        public OrderService(IOrderRepository repo, ICustomerProfileRepository customerRepo, IProductRepository productRepo)
        {
            _repo = repo;
            _customerRepo = customerRepo;
            _productRepo = productRepo;
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

        public async Task<object> GetSalesReportAsync(string shopperId)
        {
            var orders = await _repo.GetOrdersByShopperAsync(shopperId);
            var productSales = new Dictionary<string, (string itemName, int quantity, decimal revenue)>();

            int totalOrders = orders.Count;
            decimal totalSales = 0;

            foreach (var order in orders)
            {
                foreach (var item in order.Items)
                {
                    var product = await _productRepo.GetMeatByIdAsync(item.ProductId);
                    if (product != null && product.UserId == shopperId)
                    {
                        decimal price = decimal.TryParse(item.Price, out var p) ? p : 0;
                        int qty = item.Quantity;
                        decimal itemRevenue = price * qty;
                        totalSales += itemRevenue;

                        if (productSales.ContainsKey(item.ProductId))
                        {
                            var existing = productSales[item.ProductId];
                            productSales[item.ProductId] = (existing.itemName, existing.quantity + qty, existing.revenue + itemRevenue);
                        }
                        else
                        {
                            productSales[item.ProductId] = (item.ItemName, qty, itemRevenue);
                        }
                    }
                }
            }

            return new
            {
                TotalOrders = totalOrders,
                TotalSales = totalSales,
                ProductsSold = productSales.Select(p => new
                {
                    ProductId = p.Key,
                    ItemName = p.Value.itemName,
                    QuantitySold = p.Value.quantity,
                    Revenue = p.Value.revenue
                })
            };
        }


        public async Task<(string? OrderNo, decimal? TotalBillAmount, string? DeliveryStatus)> GetOrderSummaryAsync(string orderId)
        {
            var order = await _repo.GetOrderByIdAsync(orderId);
            if (order == null) return (null, null, null);

            if (!decimal.TryParse(order.TotalBillAmount, out var totalAmount))
                totalAmount = 0;

            return (order.OrderNo, totalAmount, order.DeliveryStatus);
        }

        public async Task<bool> UpdateDeliveryStatusAsync(string orderId, string newStatus)
        {
            var order = await _repo.GetOrderByIdAsync(orderId);
            if (order == null) return false;

            order.DeliveryStatus = newStatus;
            return await _repo.UpdateAsync(order);
        }

        public async Task<CustomerProfile?> GetCustomerByIdAsync(string customerId)
        {
            return await _customerRepo.GetByUserIdAsync(customerId);
        }
        public async Task<List<Order>> GetOrdersByShopperAsync(string shopperId) =>
            await _repo.GetOrdersByShopperAsync(shopperId);
    }
}
