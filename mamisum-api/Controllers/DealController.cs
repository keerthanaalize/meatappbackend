using System.Security.Claims;
using mamisum_api.DTOs;
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
        public async Task<IActionResult> CreateDeal([FromForm] IFormFile imageFile, [FromForm] string title)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var deal = new Deal
            {
                Title = title,
                UserId = userId
            };

            if (imageFile != null)
            {
                deal.ImageUrl = await _imageService.UploadImageAsync(imageFile, "deals");
            }

            await _dealService.AddDealAsync(deal);
            return CreatedAtAction(nameof(GetDealById), new { id = deal.Id }, deal);
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
