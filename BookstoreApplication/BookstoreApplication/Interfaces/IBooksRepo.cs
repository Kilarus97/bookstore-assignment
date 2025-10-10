using BookstoreApplication.DTO;
using BookstoreApplication.Models;

namespace BookstoreApplication.Interfaces
{
    public interface IBooksRepo
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<List<Book>> GetAllBookDetailsAsync();
        Task<List<Book>> SearchBookDetailsAsync(BookSearchDto search);
        Task<Book?> GetBookAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
    }
}
