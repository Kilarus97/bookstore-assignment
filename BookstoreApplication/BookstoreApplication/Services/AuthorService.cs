using BookstoreApplication.DTO;
using BookstoreApplication.Exceptions;
using BookstoreApplication.Interfaces;
using BookstoreApplication.Models;


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

        public async Task<Author> GetOneAsync(int id)
        {
            var author = await _authorsRepo.GetAuthorAsync(id);
            if (author == null)
                throw new NotFoundException($"Autor sa ID-jem {id} nije pronađen.");

            return author;
        }

        public async Task<Author> CreateAsync(Author author)
        {
            await _authorsRepo.AddAuthorAsync(author);
            return author;
        }

        public async Task<Author> UpdateAsync(int id, Author author)
        {
            var existing = await _authorsRepo.GetAuthorAsync(id);
            if (existing == null)
                throw new NotFoundException($"Autor sa ID-jem {id} nije pronađen.");

            existing.FullName = author.FullName;
            await _authorsRepo.UpdateAuthorAsync(existing);
            return existing;
        }

        public async Task DeleteAsync(int id)
        {
            var author = await _authorsRepo.GetAuthorAsync(id);
            if (author == null)
                throw new NotFoundException($"Autor sa ID-jem {id} ne postoji.");

            await _authorsRepo.DeleteAuthorAsync(id);
        }
    }
}
