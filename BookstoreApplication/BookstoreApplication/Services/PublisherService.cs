using BookstoreApplication.Exceptions;
using BookstoreApplication.Interfaces;
using BookstoreApplication.Models;
using BookstoreApplication.Repository;
using Microsoft.Extensions.Logging;

namespace BookstoreApplication.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublishersRepo _publishersRepo;
        private readonly ILogger<PublisherService> _logger;

        public PublisherService(IPublishersRepo publishersRepo, ILogger<PublisherService> logger)
        {
            _publishersRepo = publishersRepo;
            _logger = logger;
        }

        public async Task<IEnumerable<Publisher>> GetAllAsync()
        {
            _logger.LogInformation("Dohvatanje svih izdavača iz baze.");
            return await _publishersRepo.GetAllPublishersAsync();
        }

        public async Task<Publisher> GetOneAsync(int id)
        {
            _logger.LogInformation("Dohvatanje izdavača sa ID-jem {Id}", id);
            var publisher = await _publishersRepo.GetPublisherAsync(id);
            if (publisher == null)
            {
                _logger.LogWarning("Izdavač sa ID-jem {Id} nije pronađen.", id);
                throw new NotFoundException($"Izdavač sa ID-jem {id} nije pronađen.");
            }

            return publisher;
        }

        public async Task<Publisher> CreateAsync(Publisher publisher)
        {
            _logger.LogInformation("Kreiranje novog izdavača: {Name}", publisher.Name);
            await _publishersRepo.AddPublisherAsync(publisher);
            _logger.LogInformation("Izdavač uspešno kreiran: {Name}", publisher.Name);
            return publisher;
        }

        public async Task<Publisher> UpdateAsync(int id, Publisher publisher)
        {
            _logger.LogInformation("Ažuriranje izdavača ID={Id}", id);
            var existing = await _publishersRepo.GetPublisherAsync(id);
            if (existing == null)
            {
                _logger.LogWarning("Izdavač sa ID-jem {Id} nije pronađen za ažuriranje.", id);
                throw new NotFoundException($"Izdavač sa ID-jem {id} nije pronađen.");
            }

            existing.Name = publisher.Name;
            existing.Website = publisher.Website;
            await _publishersRepo.UpdatePublisherAsync(existing);
            _logger.LogInformation("Izdavač ID={Id} uspešno ažuriran.", id);
            return existing;
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("Brisanje izdavača ID={Id}", id);
            var publisher = await _publishersRepo.GetPublisherAsync(id);
            if (publisher == null)
            {
                _logger.LogWarning("Izdavač sa ID-jem {Id} ne postoji za brisanje.", id);
                throw new NotFoundException($"Izdavač sa ID-jem {id} ne postoji.");
            }

            await _publishersRepo.DeletePublisherAsync(id);
            _logger.LogInformation("Izdavač ID={Id} uspešno obrisan.", id);
        }
    }
}
