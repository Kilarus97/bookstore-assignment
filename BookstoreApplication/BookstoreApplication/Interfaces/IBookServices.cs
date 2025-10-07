using BookstoreApplication.DTO;
using BookstoreApplication.Models;

namespace BookstoreApplication.Interfaces
{
    public interface IBookServices
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDetailsDto?> GetBookDetailsAsync(int id);

        Task<Book> CreateBookAsync(Book book);
        Task<Book> UpdateBookAsync(int id, Book book);
        Task DeleteBookAsync(int id);
    }
}
