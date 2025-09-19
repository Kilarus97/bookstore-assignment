using BookstoreApplication.Data;
using BookstoreApplication.Models;
using Microsoft.AspNetCore.Mvc;
using BookstoreApplication.Repository;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public IActionResult GetAll()
        {
            return Ok(_bookRepository.GetAllBooks());
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            var book = _bookRepository.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // POST api/books
        [HttpPost]
        public IActionResult Post(Book book)
        {
            // kreiranje knjige je moguće ako je izabran postojeći autor
            var author = _authorRepository.GetAuthor(book.AuthorId);
            if (author == null)
            {
                return BadRequest("Author not found.");
            }

            // kreiranje knjige je moguće ako je izabran postojeći izdavač
            var publisher = _publisherRepository.GetPublisher(book.PublisherId);
            if (publisher == null)
            {
                return BadRequest("Publisher not found.");
            }

            book.Author = author;
            book.Publisher = publisher;
            _bookRepository.AddBook(book);
            return Ok(book);
        }

        // PUT api/books/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            var existingBook = _bookRepository.GetBook(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            // izmena knjige je moguca ako je izabran postojeći autor
            var author = _authorRepository.GetAuthor(book.AuthorId);
            if (author == null)
            {
                return BadRequest();
            }

            // izmena knjige je moguca ako je izabran postojeći izdavač
            var publisher = _publisherRepository.GetPublisher(book.PublisherId);
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
            _bookRepository.UpdateBook(existingBook);
 
            return Ok(existingBook);
        }

        // DELETE api/books/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bookRepository.DeleteBook(id);
            return NoContent();
        }
    }
}
