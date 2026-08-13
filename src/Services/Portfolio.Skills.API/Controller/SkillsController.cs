using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Shared.Contracts.Requests;
using Portfolio.Skills.API.Interfaces;

namespace Portfolio.Skills.API.Controller
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _service;

        public SkillsController(
            ISkillService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateSkillRequest request)
        {
            return Ok(
                await _service.CreateAsync(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(
                await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result =
                await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            Guid id,
            UpdateSkillRequest request)
        {
            var result =
                await _service.UpdateAsync(
                    id,
                    request);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted =
                await _service.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}

