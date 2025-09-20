using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BooksRepo _bookRepository;
        private readonly AuthorsRepo _authorRepository;
        private readonly PublishersRepo _publisherRepository;

        public AuthorsController(BooksRepo bookRepository, AuthorsRepo authorRepository, PublishersRepo publisherRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _publisherRepository = publisherRepository;
        }

        // GET: api/authors
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _authorRepository.GetAllAuthorDtosAsync();
            return Ok(authors);
        }

        // GET api/authors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var author = await _authorRepository.GetAuthorAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        // POST api/authors
        [HttpPost]
        public async Task<IActionResult> Post(Author author)
        {
            await _authorRepository.AddAuthorAsync(author);
            return Ok(author);
        }

        // PUT api/authors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            var existingAuthor = await _authorRepository.GetAuthorAsync(id);
            if (existingAuthor == null)
            {
                return NotFound();
            }

            await _authorRepository.UpdateAuthorAsync(author);
            return Ok(author);
        }

        // DELETE api/authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _authorRepository.DeleteAuthorAsync(id);
            return NoContent();
        }
    }
}
