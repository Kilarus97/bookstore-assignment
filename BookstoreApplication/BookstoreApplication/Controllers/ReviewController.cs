using BookstoreApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ReviewDto dto)
        {
            if (dto.Rating < 1 || dto.Rating > 5)
                return BadRequest("Ocena mora biti između 1 i 5.");

            var success = await _reviewService.AddReviewAsync(dto);
            if (!success)
                return StatusCode(500, "Greška prilikom upisa recenzije.");

            return Ok("Recenzija uspešno dodata.");
        }

        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetReviewsForBook(int bookId)
        {
            var reviews = await _reviewService.GetReviewsForBookAsync(bookId);
            return Ok(reviews);
        }
    }
}
