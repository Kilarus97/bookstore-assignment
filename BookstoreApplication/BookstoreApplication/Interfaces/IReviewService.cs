namespace BookstoreApplication.Interfaces
{
    public interface IReviewService
    {
        Task<bool> AddReviewAsync(ReviewDto dto);
        Task<List<Review>> GetReviewsForBookAsync(int bookId);
    }
}
