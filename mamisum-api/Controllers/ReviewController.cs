using mamisum_api.DTOs;
using mamisum_api.Mappers;
using mamisum_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace mamisum_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _service;

        public ReviewController(ReviewService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _service.GetAllAsync();
            return Ok(reviews.Select(ReviewMapper.ToDTO));
        }

        [HttpGet("get-reviews/shopper/{shopperId}")]
        public async Task<IActionResult> GetReviewsByShopper(string shopperId)
        {
            var result = await _service.GetReviewsByShopperIdWithDetailsAsync(shopperId);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var review = await _service.GetByIdAsync(id);
            return review == null ? NotFound() : Ok(ReviewMapper.ToDTO(review));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReviewDTO dto)
        {
            var model = ReviewMapper.ToModel(dto);
            await _service.CreateAsync(model);
            return Ok("Review created.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ReviewDTO dto)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            var updated = ReviewMapper.ToModel(dto);
            updated.Id = id;

            var result = await _service.UpdateAsync(updated);
            return result ? Ok("Updated") : BadRequest("Update failed");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? Ok("Deleted") : NotFound();
        }

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetByProduct(string productId)
        {
            var reviews = await _service.GetByProductIdAsync(productId);
            return Ok(reviews.Select(ReviewMapper.ToDTO));
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(string userId)
        {
            var reviews = await _service.GetByUserIdAsync(userId);
            return Ok(reviews.Select(ReviewMapper.ToDTO));
        }
    }
}
