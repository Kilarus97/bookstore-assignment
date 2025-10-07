using BookstoreApplication.Interfaces;
using BookstoreApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var publishers = await _publisherService.GetAllAsync();
            return Ok(publishers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var publisher = await _publisherService.GetOneAsync(id);
            return Ok(publisher); // Izuzetak se baca ako nije pronađen
        }

        [HttpPost]
        public async Task<IActionResult> Post(Publisher publisher)
        {
            var created = await _publisherService.CreateAsync(publisher);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Publisher publisher)
        {
            if (id != publisher.Id)
                return BadRequest("ID izdavača u URL-u ne odgovara ID-ju u telu zahteva.");

            var updated = await _publisherService.UpdateAsync(id, publisher);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _publisherService.DeleteAsync(id);
            return NoContent();
        }
    }
}
