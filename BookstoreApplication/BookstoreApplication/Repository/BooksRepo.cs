using BookstoreApplication.Data;
using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repository
{
    public class BooksRepo
    {
        private BookstoreDbContext _context;

        public BooksRepo(BookstoreDbContext context)
        {
            _context = context;
        }

        // Implement CRUD operations for Book entity here

        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .ToList();
        }

        public Book GetBook(int id)
        {
            return _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .FirstOrDefault(b => b.Id == id);
        }

        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }

        public void DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }


    }
}
