using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Profile.API.Interfaces;
using Portfolio.Shared.Contracts.Requests;

namespace Portfolio.Profile.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {

    private readonly IProfileService _service;

        public ProfileController(
            IProfileService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateProfileRequest request)
        {
            var result =
                await _service.CreateAsync(request);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            Guid id,
            UpdateProfileRequest request)
        {
            var result =
                await _service.UpdateAsync(id, request);

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

