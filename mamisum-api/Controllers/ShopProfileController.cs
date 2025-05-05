using mamisum_api.Models.Users;
using mamisum_api.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mamisum_api.Models;
using mamisum_api.DTOs;
using mamisum_api.Mappers;
using mamisum_api.Repositories;


namespace mamisum_api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ShopProfileController : ControllerBase
    {
        private readonly ShopProfileService _service;
        private readonly ImageService _imageService;
        private readonly ICustomerProfileRepository _customerRepo;
        public ShopProfileController(ShopProfileService service, ImageService imageService, ICustomerProfileRepository customerRepo)
        {
            _service = service;
            _imageService = imageService;
            _customerRepo = customerRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        /* [AllowAnonymous]
         [HttpGet("nearby")]
         public async Task<IActionResult> GetNearbyShops()
         {
             var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
             if (string.IsNullOrEmpty(userId))
                 return Unauthorized("User not found");

             var result = await _service.GetNearbyShopsAsync(userId, _customerRepo);
             return Ok(result);
         }*/

        [AllowAnonymous]
        [HttpGet("by-city")]
        public async Task<IActionResult> GetShopsByCity([FromQuery] string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                return BadRequest("City name is required");

            var shops = await _service.GetShopsByCityAsync(city);
            return Ok(shops);
        }

        [HttpGet("nearby-shops")]
        public async Task<IActionResult> GetNearbyShops([FromQuery] double maxDistance = 5)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var shops = await _service.GetNearbyShopsByLocationAsync(userId, _customerRepo, maxDistance);
            return Ok(shops);
        }


        [AllowAnonymous]
        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetByCategory(string category)
        {
            var result = await _service.GetByCategoryAsync(category);
            return Ok(result);
        }

        [HttpGet("my-profile")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var profile = await _service.GetByUserIdAsync(userId);
            return profile == null ? NotFound("Profile not found.") : Ok(profile);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateShopProfileDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var profile = await ShopProfileMapper.ToShopProfileAsync(dto, _imageService, userId);
            await _service.CreateAsync(profile);

            return CreatedAtAction(nameof(GetById), new { id = profile.Id }, profile);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromForm] CreateShopProfileDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();
            var profile = await ShopProfileMapper.ToShopProfileAsync(dto, _imageService, userId);
            profile.Id = id;
            var updated = await _service.UpdateAsync(id, profile);
            return updated ? NoContent() : NotFound();
        }
    }
}
