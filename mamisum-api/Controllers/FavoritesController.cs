using System.Security.Claims;
using mamisum_api.DTOs;
using mamisum_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace mamisum_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesController : ControllerBase
    {
        private readonly FavoriteService _service;

        public FavoritesController(FavoriteService service)
        {
            _service = service;
        }

        [HttpGet("my-favorites")]
        public async Task<IActionResult> GetUserFavorites()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var favorites = await _service.GetUserFavoritesAsync(UserId);
            return Ok(favorites);
        }

        [HttpPost("toggle")]
        public async Task<IActionResult> ToggleFavorite([FromBody] ToggleFavoriteRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _service.ToggleFavoriteAsync(userId, request.ProductId, request.ShopId);
            return Ok(new { success = result });
        }

        //[Authorize]
        //[HttpDelete("remove")]
        //public async Task<IActionResult> RemoveFavorite([FromQuery] string? productId, [FromQuery] string? shopId)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    if (string.IsNullOrEmpty(userId))
        //        return Unauthorized("User not found");

        //    if (string.IsNullOrEmpty(productId) && string.IsNullOrEmpty(shopId))
        //        return BadRequest("Provide either a ProductId or ShopId to remove");

        //    var success = await _service.RemoveFavoriteAsync(userId, productId, shopId);
        //    if (!success)
        //        return NotFound("Favorite not found");

        //    return Ok("Favorite removed successfully");
        //}

    }
}
