using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Projects.API.Interfaces;
using Portfolio.Shared.Contracts.Requests;

namespace Portfolio.Projects.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _service;

        public ProjectsController(
            IProjectService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateProjectRequest request)
        {
            return Ok(await _service.CreateAsync(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var project = await _service.GetByIdAsync(id);

            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            Guid id,
            UpdateProjectRequest request)
        {
            var project =
                await _service.UpdateAsync(id, request);

            if (project == null)
                return NotFound();

            return Ok(project);
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
