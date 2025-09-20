using BookstoreApplication.Data;
using BookstoreApplication.Models;
using Microsoft.AspNetCore.Mvc;
using BookstoreApplication.Repository;
using System.Threading.Tasks;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksRepo _bookRepository;
        private readonly AuthorsRepo _authorRepository;
        private readonly PublishersRepo _publisherRepository;

        public BooksController(BooksRepo bookRepository, AuthorsRepo authorRepository, PublishersRepo publisherRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _publisherRepository = publisherRepository;
        }

        // GET: api/books
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return Ok(books);
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var book = await _bookRepository.GetBookAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // POST api/books
        [HttpPost]
        public async Task<IActionResult> Post(Book book)
        {
            // kreiranje knjige je moguće ako je izabran postojeći autor
            var author = await _authorRepository.GetAuthorAsync(book.AuthorId);
            if (author == null)
            {
                return BadRequest("Author not found.");
            }

            // kreiranje knjige je moguće ako je izabran postojeći izdavač
            var publisher = await _publisherRepository.GetPublisherAsync(book.PublisherId);
            if (publisher == null)
            {
                return BadRequest("Publisher not found.");
            }

            book.Author = author;
            book.Publisher = publisher;
            await _bookRepository.AddBookAsync(book);
            return Ok(book);
        }

        // PUT api/books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            var existingBook = await _bookRepository.GetBookAsync(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            // izmena knjige je moguca ako je izabran postojeći autor
            var author = await _authorRepository.GetAuthorAsync(book.AuthorId);
            if (author == null)
            {
                return BadRequest();
            }

            // izmena knjige je moguca ako je izabran postojeći izdavač
            var publisher = await _publisherRepository.GetPublisherAsync(book.PublisherId);
            if (publisher == null)
            {
                return BadRequest();
            }

            existingBook.Title = book.Title;
            existingBook.PageCount = book.PageCount;
            existingBook.PublishedDate = book.PublishedDate;
            existingBook.ISBN = book.ISBN;
            existingBook.Author = author;
            existingBook.AuthorId = author.Id;
            existingBook.Publisher = publisher;
            existingBook.PublisherId = publisher.Id;
            await _bookRepository.UpdateBookAsync(existingBook);

            return Ok(existingBook);
        }

        // DELETE api/books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookRepository.DeleteBookAsync(id);
            return NoContent();
        }
    }
}
