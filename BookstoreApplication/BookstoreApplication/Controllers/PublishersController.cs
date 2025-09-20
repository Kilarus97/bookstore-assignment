using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly BooksRepo _bookRepository;
        private readonly AuthorsRepo _authorRepository;
        private readonly PublishersRepo _publisherRepository;

        public PublishersController(BooksRepo bookRepository, AuthorsRepo authorRepository, PublishersRepo publisherRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _publisherRepository = publisherRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var publishers = await _publisherRepository.GetAllPublishersAsync();
            return Ok(publishers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var publisher = await _publisherRepository.GetPublisherAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return Ok(publisher);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Publisher publisher)
        {
            await _publisherRepository.AddPublisherAsync(publisher);
            return Ok(publisher);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return BadRequest();
            }

            var existingPublisher = await _publisherRepository.GetPublisherAsync(id);
            if (existingPublisher == null)
            {
                return NotFound();
            }

            // Optionally map fields from 'publisher' to 'existingPublisher' here
            await _publisherRepository.UpdatePublisherAsync(publisher);
            return Ok(publisher);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _publisherRepository.DeletePublisherAsync(id);
            return NoContent();
        }
    }
}
