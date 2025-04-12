using System.Security.Claims;
using mamisum_api.Models;
using mamisum_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace mamisum_api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MeatDetailsController : ControllerBase
    {
        private readonly MeatDetailService _meatService;

        public MeatDetailsController(MeatDetailService meatService)
        {
            _meatService = meatService;
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetMeatsByCategory(string categoryId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var meats = await _meatService.GetMeatsByCategoryAsync(categoryId, userId);
            return Ok(meats);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMeat([FromBody] MeatDetail meat)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            meat.UserId = userId; // Automatically attach user ID
            await _meatService.AddMeatAsync(meat);
            return CreatedAtAction(nameof(GetMeatById), new { id = meat.Id }, meat);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeatById(string id)
        {
            var meat = await _meatService.GetMeatByIdAsync(id);
            return meat == null ? NotFound() : Ok(meat);
        }

        
        [HttpGet("my-meats")]
        public async Task<IActionResult> GetMyMeats()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Message = "User not authorized" });
            }

            var meats = await _meatService.GetMeatsByUserIdAsync(userId);
            return Ok(meats);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeat(string id, [FromBody] MeatDetail meat)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            meat.Id = id;
            meat.UserId = userId;

            var result = await _meatService.UpdateMeatAsync(meat);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeat(string id)
        {
            var result = await _meatService.DeleteMeatAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
