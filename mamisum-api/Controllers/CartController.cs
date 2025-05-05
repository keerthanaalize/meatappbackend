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

            var cartWithFav = await _cartService.GetCartWithFavoriteAsync(userId);
            return Ok(cartWithFav);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] Cart cart)
        {
            cart.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _cartService.AddToCartAsync(cart);
            return Ok(cart);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(string id, [FromBody] Cart cart)
        {
            cart.Id = id;
            var success = await _cartService.UpdateCartAsync(cart);
            return success ? Ok(cart) : NotFound();
        }
         
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromCart(string id)
        {
            var success = await _cartService.RemoveFromCartAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
