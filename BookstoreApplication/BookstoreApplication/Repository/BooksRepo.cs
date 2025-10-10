using BookstoreApplication.Data;
using BookstoreApplication.DTO;
using BookstoreApplication.Interfaces;
using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repository
{
    public class BooksRepo : IBooksRepo
    {
        private readonly BookstoreDbContext _context;

        public BooksRepo(BookstoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> SearchBookDetailsAsync(BookSearchDto search)
        {
            var query = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search.TitleContains))
                query = query.Where(b => b.Title.ToLower().Contains(search.TitleContains.ToLower()));

            if (search.PublishedFrom.HasValue)
                query = query.Where(b => b.PublishedDate >= search.PublishedFrom.Value);

            if (search.PublishedTo.HasValue)
                query = query.Where(b => b.PublishedDate <= search.PublishedTo.Value);

            if (search.AuthorId.HasValue)
                query = query.Where(b => b.AuthorId == search.AuthorId.Value);

            if (!string.IsNullOrWhiteSpace(search.AuthorNameContains))
                query = query.Where(b => b.Author != null && b.Author.FullName.ToLower().Contains(search.AuthorNameContains.ToLower()));

            if (search.AuthorBornFrom.HasValue)
                query = query.Where(b => b.Author != null && b.Author.DateOfBirth >= search.AuthorBornFrom.Value);

            if (search.AuthorBornTo.HasValue)
                query = query.Where(b => b.Author != null && b.Author.DateOfBirth <= search.AuthorBornTo.Value);

            if (!string.IsNullOrWhiteSpace(search.SortType))
            {
                query = search.SortType switch
                {
                    "TitleAsc" => query.OrderBy(b => b.Title),
                    "TitleDesc" => query.OrderByDescending(b => b.Title),
                    "PublishDateAsc" => query.OrderBy(b => b.PublishedDate),
                    "PublishDateDesc" => query.OrderByDescending(b => b.PublishedDate),
                    "AuthorNameAsc" => query.OrderBy(b => b.Author.FullName),
                    "AuthorNameDesc" => query.OrderByDescending(b => b.Author.FullName),
                    _ => query.OrderBy(b => b.Title)
                };
            }

            return await query.ToListAsync();
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .ToListAsync();
        }

        public async Task<List<Book>> GetAllBookDetailsAsync()
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .ToListAsync();
        }

        public async Task<Book?> GetBookAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}
