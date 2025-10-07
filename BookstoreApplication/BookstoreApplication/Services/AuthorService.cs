using BookstoreApplication.DTO;
using BookstoreApplication.Exceptions;
using BookstoreApplication.Interfaces;
using BookstoreApplication.Models;
using BookstoreApplication.Repository;
using Microsoft.Extensions.Logging;

namespace BookstoreApplication.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorsRepo _authorsRepo;
        private readonly ILogger<AuthorService> _logger;

        public AuthorService(IAuthorsRepo authorsRepo, ILogger<AuthorService> logger)
        {
            _authorsRepo = authorsRepo;
            _logger = logger;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAsync()
        {
            _logger.LogInformation("Dohvatanje svih autora iz baze.");
            return await _authorsRepo.GetAllAuthorDtosAsync();
        }

        public async Task<Author> GetOneAsync(int id)
        {
            _logger.LogInformation("Dohvatanje autora sa ID-jem {Id}", id);
            var author = await _authorsRepo.GetAuthorAsync(id);
            if (author == null)
            {
                _logger.LogWarning("Autor sa ID-jem {Id} nije pronađen.", id);
                throw new NotFoundException($"Autor sa ID-jem {id} nije pronađen.");
            }

            return author;
        }

        public async Task<Author> CreateAsync(Author author)
        {
            _logger.LogInformation("Kreiranje novog autora: {FullName}", author.FullName);
            await _authorsRepo.AddAuthorAsync(author);
            _logger.LogInformation("Autor uspešno kreiran: {FullName}", author.FullName);
            return author;
        }

        public async Task<Author> UpdateAsync(int id, Author author)
        {
            _logger.LogInformation("Ažuriranje autora ID={Id}", id);
            var existing = await _authorsRepo.GetAuthorAsync(id);
            if (existing == null)
            {
                _logger.LogWarning("Autor sa ID-jem {Id} nije pronađen za ažuriranje.", id);
                throw new NotFoundException($"Autor sa ID-jem {id} nije pronađen.");
            }

            existing.FullName = author.FullName;
            await _authorsRepo.UpdateAuthorAsync(existing);
            _logger.LogInformation("Autor ID={Id} uspešno ažuriran.", id);
            return existing;
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("Brisanje autora ID={Id}", id);
            var author = await _authorsRepo.GetAuthorAsync(id);
            if (author == null)
            {
                _logger.LogWarning("Autor sa ID-jem {Id} ne postoji za brisanje.", id);
                throw new NotFoundException($"Autor sa ID-jem {id} ne postoji.");
            }

            await _authorsRepo.DeleteAuthorAsync(id);
            _logger.LogInformation("Autor ID={Id} uspešno obrisan.", id);
        }
    }
}
