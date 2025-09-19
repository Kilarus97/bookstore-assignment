using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public IActionResult GetAll()
        {
            return Ok(_authorRepository.GetAllAuthorDtos());
        }

        // GET api/authors/5
        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            var author = _authorRepository.GetAuthor(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        // POST api/authors
        [HttpPost]
        public IActionResult Post(Author author)
        {
            _authorRepository.UpdateAuthor(author);
            return Ok(author);
        }

        // PUT api/authors/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            var existingAuthor = _authorRepository.GetAuthor(id);
            if (existingAuthor == null)
            {
                return NotFound();
            }

            existingAuthor = author;

            _authorRepository.UpdateAuthor(existingAuthor);
            return Ok(author);
        }

        // DELETE api/authors/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            _authorRepository.DeleteAuthor(id);
            return NoContent();
        }
    }
}
