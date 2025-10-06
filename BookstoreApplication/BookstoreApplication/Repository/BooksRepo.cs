using BookstoreApplication.Data;
using BookstoreApplication.DTO;
using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repository
{
    public class BooksRepo
    {
        private readonly BookstoreDbContext _context;

        public BooksRepo(BookstoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            return await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                PageCount = b.PageCount,
                PublishedDate = b.PublishedDate,
                ISBN = b.ISBN,
                Author = b.Author != null ? b.Author.FullName : string.Empty,
                Publisher = b.Publisher != null ? b.Publisher.Name : string.Empty,
                Website = b.Publisher != null ? b.Publisher.Website : string.Empty
            })
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