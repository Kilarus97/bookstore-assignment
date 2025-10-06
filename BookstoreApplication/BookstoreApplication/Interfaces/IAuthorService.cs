using BookstoreApplication.DTO;
using BookstoreApplication.Models;

namespace BookstoreApplication.Interfaces

{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAsync();
        Task<Author?> GetOneAsync(int id);
        Task<Author> CreateAsync(Author author);
        Task<Author> UpdateAsync(int id, Author author);
        Task DeleteAsync(int id);
    }
}
