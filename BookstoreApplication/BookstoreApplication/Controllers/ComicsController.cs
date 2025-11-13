using BookstoreApplication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComicsController : ControllerBase
    {
        private readonly IComicService _service;

        public ComicsController(IComicService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Editor")]
        [HttpGet("volumes")]
        public async Task<IActionResult> SearchVolumes([FromQuery] string name)
        {
            var result = await _service.SearchVolumes(name);
            return Ok(result);
        }

        [Authorize(Roles = "Editor")]
        [HttpGet("issues")]
        public async Task<IActionResult> GetIssues([FromQuery] int volumeId)
        {
            var result = await _service.GetIssues(volumeId);
            return Ok(result);
        }

        [Authorize(Roles = "Editor")]
        [HttpPost("create-local-issue")]
        public async Task<IActionResult> CreateLocalIssue([FromBody] CreateIssueDto dto)
        {
            var id = await _service.CreateIssueFromExternalAsync(dto);
            return CreatedAtAction(nameof(GetIssue), new { id }, id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIssue(int id)
        {
            var issue = await _service.GetIssueByIdAsync(id);
            if (issue == null) return NotFound();
            return Ok(issue);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLocalIssues()
        {
            var issues = await _service.GetAllLocalIssuesAsync();
            return Ok(issues);
        }

    }

}
