using System.Security.Claims;
using mamisum_api.DTOs;
using mamisum_api.Models.Users;
using mamisum_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace mamisum_api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CustomerProfileController : ControllerBase
    {
        private readonly CustomerProfileService _service;

        public CustomerProfileController(CustomerProfileService service)
        {
            _service = service;
        }

        [HttpGet("current-userprofile")] 
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profile = await _service.GetProfileAsync(userId);
            if (profile == null) return NotFound();
            return Ok(profile);
        }

        [HttpPost("create-userprofile")]
        public async Task<IActionResult> Create([FromForm] CreateCustomerProfileDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _service.CreateProfileAsync(dto, userId);
            return Ok(new { message = "Profile created successfully." });
        }

        [HttpPut("update-userprofile")]
        public async Task<IActionResult> Update([FromForm] UpdateCustomerProfileDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var success = await _service.UpdateProfileAsync(dto, userId);
            if (!success) return NotFound();
            return Ok(new { message = "Profile updated successfully." });
        }
    }
}
