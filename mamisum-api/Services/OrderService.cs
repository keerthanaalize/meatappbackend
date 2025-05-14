using mamisum_api.DTOs;
using mamisum_api.Helpers;
using mamisum_api.Models;
using mamisum_api.Models.Users;
using mamisum_api.Repositories;
using MongoDB.Bson;

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

        public async Task<List<SalesReportItemDto>> GetSalesReportAsync(string shopperId)
        {
            var orders = await _repo.GetOrdersByShopperAsync(shopperId);
            var report = new List<SalesReportItemDto>();

            foreach (var order in orders)
            {
                var reportItem = new SalesReportItemDto
                {
                    OrderNo = order.OrderNo,
                    OrderDate = order.Id.CreationTimeFromObjectId(), 
                    SGST = decimal.TryParse(order.SGST, out var sgst) ? sgst : 0,
                    CGST = decimal.TryParse(order.CGST, out var cgst) ? cgst : 0,
                    TotalBillAmount = decimal.TryParse(order.TotalBillAmount, out var total) ? total : 0
                };

                foreach (var item in order.Items)
                {
                    var product = await _productRepo.GetMeatByIdAsync(item.ProductId);
                    if (product != null && product.UserId == shopperId)
                    {
                        decimal price = decimal.TryParse(item.Price, out var p) ? p : 0;
                        int qty = item.Quantity;
                        decimal itemTotal = price * qty;

                        reportItem.Products.Add(new SalesProductDto
                        {
                            ProductId = item.ProductId,
                            ProductName = item.ItemName,
                            Quantity = qty,
                            Price = price,
                            Total = itemTotal
                        });
                    }
                }

                if (reportItem.Products.Any())
                {
                    report.Add(reportItem);
                }
            }

            return report;
        }

        public async Task<List<object>> GetShopperOrderSummariesAsync(string shopperId)
        {
            var orders = await _repo.GetOrdersByShopperAsync(shopperId);

            var summaries = orders.Select(order =>
            {
                decimal.TryParse(order.TotalBillAmount, out var totalAmount);

                return new
                {
                    Id = order.Id,
                    OrderNo = order.OrderNo,
                    TotalBillAmount = totalAmount,
                    DeliveryStatus = order.DeliveryStatus
                };
            }).ToList<object>();

            return summaries;
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

        public async Task<List<Order>> GetOrdersByCustomerAndStatusAsync(string customerId, string status)
        {
            var orders = await _repo.GetOrdersByCustomerIdAsync(customerId);
            return orders.Where(o => string.Equals(o.DeliveryStatus, status, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public async Task<List<Order>> GetOrdersByShopperAndStatusAsync(string shopperId, string status)
        {
            var orders = await _repo.GetOrdersByShopperAsync(shopperId);
            return orders.Where(o => string.Equals(o.DeliveryStatus, status, StringComparison.OrdinalIgnoreCase)).ToList();
        }


        public async Task<List<Order>> GetOrdersByShopperAsync(string shopperId) =>
            await _repo.GetOrdersByShopperAsync(shopperId);
    }
}
