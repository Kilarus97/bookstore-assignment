using BookstoreApplication.DTO;
using BookstoreApplication.Interfaces;
using BookstoreApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _authorService.GetAllAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var author = await _authorService.GetOneAsync(id);
            return Ok(author); // Izuzetak se baca ako nije pronađen
        }

        [HttpGet("paginated")]
        public async Task<ActionResult<PaginatedList<AuthorDto>>> GetPaginatedAuthors([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var result = await _authorService.GetPaginatedAsync(pageIndex, pageSize);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Post(Author author)
        {
            var created = await _authorService.CreateAsync(author);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Author author)
        {
            if (id != author.Id)
                return BadRequest("ID autora u URL-u ne odgovara ID-ju u telu zahteva.");

            var updated = await _authorService.UpdateAsync(id, author);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _authorService.DeleteAsync(id);
            return NoContent();
        }
    }
}
