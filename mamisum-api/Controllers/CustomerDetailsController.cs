using System.Security.Claims;
using mamisum_api.Models.Users;
using mamisum_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace mamisum_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDetailsController : ControllerBase
    {
          private readonly CustomerDetailsService _customerDetailsService;

        public CustomerDetailsController(CustomerDetailsService customerDetailsService)
        {
            _customerDetailsService = customerDetailsService;
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCustomerDetails()
        {
            var userId = GetUserId();
            var customerDetails = await _customerDetailsService.GetCustomerDetailsByUserId(userId);

            if (customerDetails == null)
                return NotFound(new { Success = false, Message = "Customer details not found" });

            return Ok(new { customerDetails });
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetCustomerDetailsById(string id)
        {
            var customerDetails = await _customerDetailsService.GetCustomerDetailsByUserId(id);

            if (customerDetails == null)
                return NotFound(new { Success = false, Message = "Customer details not found" });

            return Ok(new {customerDetails});
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrUpdateCustomerDetails([FromBody] CustomerDetails request)
        {
            var userId = GetUserId();
            var isUpdated = await _customerDetailsService.UpsertCustomerDetails(userId, request);

            if (isUpdated)
                return Ok(new { Success = true, Message = "Customer details saved successfully" });

            return BadRequest(new { Success = false, Message = "Failed to save customer details" });
        }


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateCustomerDetails([FromBody] CustomerDetails request)
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Success = false, Message = "Invalid or missing user ID" });
            }

            var isUpdated = await _customerDetailsService.UpsertCustomerDetails(userId, request);

            if (isUpdated)
                return Ok(new { Success = true, Message = "Customer details updated successfully" });

            return BadRequest(new { Success = false, Message = "Failed to update customer details" });
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteCustomerDetails()
        {
            var userId = GetUserId();
            var isDeleted = await _customerDetailsService.DeleteCustomerDetails(userId);

            if (isDeleted)
                return Ok(new { Success = true, Message = "Customer details deleted successfully" });

            return NotFound(new { Success = false, Message = "Customer details not found" });
        }
    }
}
