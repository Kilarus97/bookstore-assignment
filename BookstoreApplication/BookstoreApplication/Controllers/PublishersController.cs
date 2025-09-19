using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Repository;
using Microsoft.AspNetCore.Mvc;

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

        // GET: api/publishers
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_publisherRepository.GetAllPublishers());
        }

        // GET api/publishers/5
        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            var publisher = _publisherRepository.GetPublisher(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return Ok(publisher);
        }

        // POST api/publishers
        [HttpPost]
        public IActionResult Post(Publisher publisher)
        {
            _publisherRepository.AddPublisher(publisher);
            return Ok(publisher);
        }

        // PUT api/publishers/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return BadRequest();
            }

            var existingPublisher = _publisherRepository.GetPublisher(id);
            if (existingPublisher == null)
            {
                return NotFound();
            }


            existingPublisher = publisher;
            _publisherRepository.UpdatePublisher(existingPublisher);
            return Ok(publisher);
        }

        // DELETE api/publishers/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _publisherRepository.DeletePublisher(id);
            return NoContent();
        }
    }
}
