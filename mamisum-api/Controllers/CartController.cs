using mamisum_api.Models;
using mamisum_api.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace mamisum_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var cartWithFavorites = await _cartService.GetCartWithFavoriteAsync(userId);
            return Ok(cartWithFavorites);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] Cart cart)
        {
            cart.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _cartService.AddToCartAsync(cart);
            return Ok(cart);
        }

        [HttpPost("{id}/voice-message")]
        public async Task<IActionResult> UploadVoiceMessage(string id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid audio file");

            if (!file.ContentType.StartsWith("audio/"))
                return BadRequest("Only audio files are allowed");

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var folderPath = Path.Combine("wwwroot", "voice_messages");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var voiceMessageUrl = $"{baseUrl}/voice_messages/{fileName}";

            var cart = await _cartService.GetCartByIdAsync(id);
            if (cart == null)
                return NotFound();

            cart.VoiceMessageUrl = voiceMessageUrl;
            var updated = await _cartService.UpdateCartAsync(cart);

            return updated ? Ok(new { voiceMessageUrl }) : StatusCode(500, "Failed to update cart");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(string id, [FromBody] Cart cart)
        {
            cart.Id = id;
            var success = await _cartService.UpdateCartAsync(cart);
            return success ? Ok(cart) : NotFound();
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var success = await _cartService.ClearCartAsync(userId);
            return success ? NoContent() : NotFound();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromCart(string id)
        {
            var success = await _cartService.RemoveFromCartAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
