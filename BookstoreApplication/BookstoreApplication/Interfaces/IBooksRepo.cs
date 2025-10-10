using BookstoreApplication.DTO;
using BookstoreApplication.Models;

namespace BookstoreApplication.Interfaces
{
    public interface IBooksRepo
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<IEnumerable<BookDetailsDto>> GetAllBookDetailsAsync();
        Task<List<BookDetailsDto>> SearchBookDetailsAsync(BookSearchDto search);
        Task<Book?> GetBookAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
    }
}