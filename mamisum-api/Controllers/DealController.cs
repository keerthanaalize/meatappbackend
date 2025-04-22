using System.Security.Claims;
using mamisum_api.DTOs;
using mamisum_api.Mappers;
using mamisum_api.Models;
using mamisum_api.Models.Users;
using mamisum_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace mamisum_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealController : ControllerBase
    {
        private readonly DealService _dealService;
        private readonly ImageService _imageService;

        public DealController(DealService dealService, ImageService imageService)
        {
            _dealService = dealService;
            _imageService = imageService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeal([FromForm] CreateDealDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            await _dealService.CreateDealAsync(dto, userId);

            return CreatedAtAction(nameof(GetDealById), new { id = dto.UserId }, dto);
        }


        [HttpGet("my-deals")]
        public async Task<IActionResult> GetMyDeals()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var deals = await _dealService.GetDealsByUserIdAsync(userId);
            return Ok(deals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDealById(string id)
        {
            var deal = await _dealService.GetDealByIdAsync(id);
            return deal == null ? NotFound() : Ok(deal);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeal(string id)
        {
            var result = await _dealService.DeleteDealAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
