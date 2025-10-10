using BookstoreApplication.DTO;
using BookstoreApplication.Enums;
using BookstoreApplication.Interfaces;
using BookstoreApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookServices _bookService;

        public BooksController(IBookServices bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("details/sorted")]
        public async Task<ActionResult<List<BookDetailsDto>>> GetSortedBookDetails([FromQuery] BookSortType? sortType)
        {
            var result = await _bookService.GetSortedDetailsAsync(sortType ?? BookSortType.TitleAsc);
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var book = await _bookService.GetBookDetailsAsync(id);
            return Ok(book); // Izuzetak se baca ako knjiga ne postoji
        }

        [HttpPost]
        public async Task<IActionResult> Post(Book book)
        {
            var created = await _bookService.CreateBookAsync(book);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Book book)
        {
            if (id != book.Id)
                return BadRequest("ID knjige u URL-u ne odgovara ID-ju u telu zahteva.");

            var updated = await _bookService.UpdateBookAsync(id, book);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }
    }
}
