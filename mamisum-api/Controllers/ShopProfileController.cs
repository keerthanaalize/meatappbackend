using mamisum_api.Models.Users;
using mamisum_api.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mamisum_api.Models;
using mamisum_api.DTOs;
using mamisum_api.Mappers;

namespace mamisum_api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ShopProfileController : ControllerBase
    {
        private readonly ShopProfileService _service;
        private readonly ImageService _imageService;

        public ShopProfileController(ShopProfileService service, ImageService imageService)
        {
            _service = service;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateShopProfileDto dto)
        {
            var profile = await ShopProfileMapper.ToShopProfileAsync(dto, _imageService);
            await _service.CreateAsync(profile);
            return CreatedAtAction(nameof(GetById), new { id = profile.Id }, profile);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromForm] CreateShopProfileDto dto)
        {
            var profile = await ShopProfileMapper.ToShopProfileAsync(dto, _imageService);
            profile.Id = id;
            var updated = await _service.UpdateAsync(id, profile);
            return updated ? NoContent() : NotFound();
        }
    }
}
