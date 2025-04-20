using mamisum_api.DTOs;
using mamisum_api.Mappers;
using mamisum_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace mamisum_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TotalOrderController : ControllerBase
    {
        private readonly TotalOrderService _service;

        public TotalOrderController(TotalOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var order = await _service.GetByIdAsync(id);
            return order == null ? NotFound() : Ok(order);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateTotalOrderDto dto)
        {
            var order = TotalOrderMapper.ToModel(dto);
            await _service.CreateAsync(order);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateTotalOrderDto dto)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            TotalOrderMapper.MapUpdate(existing, dto);
            var updated = await _service.UpdateAsync(existing);
            return updated ? NoContent() : StatusCode(500);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
