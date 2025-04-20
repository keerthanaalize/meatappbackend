using System.Security.Claims;
using mamisum_api.DTOs;
using mamisum_api.Mappers;
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
    public class ProductController : ControllerBase
    {
        private readonly ProductService _meatService;
        private readonly ImageService _imageService;

        public ProductController(ProductService meatService, ImageService imageService)
        {
            _meatService = meatService;
            _imageService = imageService;
        }

        [AllowAnonymous]
        [HttpGet("all-products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _meatService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("my-products")]
        public async Task<IActionResult> GetMyProducts()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var products = await _meatService.GetProductsByUserAsync(userId);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeatById(string id)
        {
            var meat = await _meatService.GetMeatByIdAsync(id);
            return meat == null ? NotFound() : Ok(meat);
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> CreateMeatWithImage([FromForm] CreateProductDto dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var meat = await ProductMapper.ToProductAsync(dto, userId, _imageService);

                await _meatService.AddMeatAsync(meat);
                return CreatedAtAction(nameof(GetMeatById), new { id = meat.Id }, meat);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeat(string id, [FromBody] Product meat)
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
