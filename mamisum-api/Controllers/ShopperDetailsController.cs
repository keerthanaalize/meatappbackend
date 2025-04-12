using mamisum_api.Models.Users;
using mamisum_api.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mamisum_api.Models;

namespace mamisum_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopperDetailsController : ControllerBase
    {
        private readonly ShopperDetailsService _shopperDetailsService;

        public ShopperDetailsController(ShopperDetailsService shopperDetailsService)
        {
            _shopperDetailsService = shopperDetailsService;
        }

        private string GetShopperId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetShopperDetails()
        {
            var shopperId = GetShopperId();
            var shopperDetails = await _shopperDetailsService.GetShopperDetailsByUserId(shopperId);

            if (shopperDetails == null)
                return NotFound(new { Success = false, Message = "Shopper details not found" });

            return Ok(new { shopperDetails });
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetShopperDetailsById(string id)
        {
            var shopperDetails = await _shopperDetailsService.GetShopperDetailsByUserId(id);

            if (shopperDetails == null)
                return NotFound(new { Success = false, Message = "Shopper details not found" });

            return Ok(new { shopperDetails });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrUpdateShopperDetails([FromBody] ShopperDetails request)
        {
            var shopperId = GetShopperId();
            var isUpdated = await _shopperDetailsService.UpsertShopperDetails(shopperId, request);

            if (isUpdated)
                return Ok(new { Success = true, Message = "Shopper details saved successfully" });

            return BadRequest(new { Success = false, Message = "Failed to save Shopper details" });
        }


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateShopperDetails([FromBody] ShopperDetails request)
        {
            var shopperId = GetShopperId();
            if (string.IsNullOrEmpty(shopperId))
            {
                return Unauthorized(new { Success = false, Message = "Invalid or missing user ID" });
            }

            var isUpdated = await _shopperDetailsService.UpsertShopperDetails(shopperId, request);

            if (isUpdated)
                return Ok(new { Success = true, Message = "Shopper details updated successfully" });

            return BadRequest(new { Success = false, Message = "Failed to update Shopper details" });
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteShopperDetails()
        {
            var shopperId = GetShopperId();
            var isDeleted = await _shopperDetailsService.DeleteShopperDetails(shopperId);

            if (isDeleted)
                return Ok(new { Success = true, Message = "Customer details deleted successfully" });

            return NotFound(new { Success = false, Message = "Customer details not found" });
        }
    }
}
