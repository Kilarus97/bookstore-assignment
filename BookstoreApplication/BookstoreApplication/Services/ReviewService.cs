using BookstoreApplication.Interfaces;

public class ReviewService : IReviewService
{
    private readonly IUnitOfWork _unitOfWork;

    public ReviewService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> AddReviewAsync(ReviewDto dto)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var review = new Review
            {
                Username = dto.Username,
                BookId = dto.BookId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Review.AddAsync(review);
            await _unitOfWork.CompleteAsync();

            var ratings = await _unitOfWork.Review.GetRatingsForBookAsync(dto.BookId);
            var average = ratings.Any() ? ratings.Average() : 0;

            var book = await _unitOfWork.Books.GetBookAsync(dto.BookId);
            if (book == null)
                throw new Exception("Book not found");

            book.AverageRating = Math.Round(average, 2);
            await _unitOfWork.CompleteAsync();

            await _unitOfWork.CommitAsync();
            return true;
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            return false;
        }
    }

    public async Task<List<Review>> GetReviewsForBookAsync(int bookId)
    {
        return await _unitOfWork.Review.GetReviewsForBookAsync(bookId);
    }
}
