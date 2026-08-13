using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Contact.API.Interfaces;
using Portfolio.Shared.Contracts.Requests;

namespace Portfolio.Contact.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _service;

        public ContactController(
            IContactService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateContactMessageRequest request)
        {
            var result =
                await _service.CreateAsync(request);

            return Ok(result);
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
    }
}
