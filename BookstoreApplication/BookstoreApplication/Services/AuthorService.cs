using BookstoreApplication.DTO;
using BookstoreApplication.Interfaces;
using BookstoreApplication.Models;
using BookstoreApplication.Repository;

namespace BookstoreApplication.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorsRepo _authorsRepo;

        public AuthorService(IAuthorsRepo authorsRepo)
        {
            _authorsRepo = authorsRepo;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAsync()
        {
            return await _authorsRepo.GetAllAuthorDtosAsync();
        }

        public async Task<Author?> GetOneAsync(int id)
        {
            return await _authorsRepo.GetAuthorAsync(id);
        }

        public async Task<Author> CreateAsync(Author author)
        {
            await _authorsRepo.AddAuthorAsync(author);
            return author;
        }

        public async Task<Author> UpdateAsync(int id, Author author)
        {
            var existing = await _authorsRepo.GetAuthorAsync(id);
            if (existing == null) throw new Exception("Author not found");

            existing.FullName = author.FullName;
            await _authorsRepo.UpdateAuthorAsync(existing);
            return existing;
        }

        public async Task DeleteAsync(int id)
        {
            await _authorsRepo.DeleteAuthorAsync(id);
        }
    }
}
