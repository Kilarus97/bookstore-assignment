public interface IReviewRepository
{
    Task AddAsync(Review review);
    Task<List<int>> GetRatingsForBookAsync(int bookId);
    Task<List<Review>> GetReviewsForBookAsync(int bookId);
}
