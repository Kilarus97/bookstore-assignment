using BookstoreApplication.DTO;
using BookstoreApplication.Models;

namespace BookstoreApplication.Interfaces
{
    public interface IAuthorsRepo
    {
        Task<IEnumerable<AuthorDto>> GetAllAuthorDtosAsync();
        Task<Author?> GetAuthorAsync(int id);
        Task AddAuthorAsync(Author author);
        Task UpdateAuthorAsync(Author author);
        Task DeleteAuthorAsync(int id);
    }
}
