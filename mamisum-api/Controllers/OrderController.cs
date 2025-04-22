using System.Security.Claims;
using mamisum_api.DTOs;
using mamisum_api.Models;
using mamisum_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace mamisum_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly OrderService _service;

        public OrderController(OrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _service.GetAllAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var order = await _service.GetByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpGet("order-with-customer/{orderId}")]
        public async Task<IActionResult> GetOrderWithCustomer(string orderId)
        {
            var order = await _service.GetOrderByIdAsync(orderId);
            if (order == null) return NotFound("Order not found");

            var customer = await _service.GetCustomerByIdAsync(order.CustomerId);
            if (customer == null) return NotFound("Customer not found");

            var dto = new OrderWithCustomerDto
            {
                OrderNo = order.OrderNo,
                Items = order.Items,
                TotalBillAmount = order.TotalBillAmount,
                OrderStatus = order.OrderStatus,
                CustomerName = customer.Name,
                CustomerEmail = customer.EmailId,
                CustomerPhone = customer.MobileNo
            };

            return Ok(dto);
        }

        [HttpGet("my-orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(customerId)) return Unauthorized();

            var orders = await _service.GetOrdersByCustomerIdAsync(customerId);
            return Ok(orders);
        }


        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            await _service.CreateAsync(order);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Order updatedOrder)
        {
            var order = await _service.GetByIdAsync(id);
            if (order == null) return NotFound();

            updatedOrder.Id = id;
            var success = await _service.UpdateAsync(updatedOrder);
            return success ? NoContent() : StatusCode(500);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }

        [Authorize]
        [HttpGet("shopper-orders")]
        public async Task<IActionResult> GetOrdersByShopper()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _service.GetOrdersByShopperAsync(userId);
            return Ok(orders);
        }
    }
}
