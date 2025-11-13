using System;
using BookstoreApplication.Data;
using Microsoft.EntityFrameworkCore;

public class ReviewRepository : IReviewRepository
{
    private readonly BookstoreDbContext _context;

    public ReviewRepository(BookstoreDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Review review)
    {
        await _context.Reviews.AddAsync(review);
    }

    public async Task<List<int>> GetRatingsForBookAsync(int bookId)
    {
        return await _context.Reviews
            .Where(r => r.BookId == bookId)
            .Select(r => r.Rating)
            .ToListAsync();
    }

    public async Task<List<Review>> GetReviewsForBookAsync(int bookId)
    {
        return await _context.Reviews
            .Where(r => r.BookId == bookId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }
}
