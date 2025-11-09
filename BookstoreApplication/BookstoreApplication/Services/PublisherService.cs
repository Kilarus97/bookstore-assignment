using BookstoreApplication.DTO;
using BookstoreApplication.Enums;
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

        public async Task<IEnumerable<PublisherDto>> GetAllAsync()
        {
            _logger.LogInformation("Dohvatanje svih izdavača iz baze.");
            var publishers = await _publishersRepo.GetAllPublishersAsync();
            return publishers.Select(MapToDto);
        }

        public async Task<PublisherDto> GetOneAsync(int id)
        {
            _logger.LogInformation("Dohvatanje izdavača sa ID-jem {Id}", id);
            var publisher = await _publishersRepo.GetPublisherAsync(id);
            if (publisher == null)
            {
                _logger.LogWarning("Izdavač sa ID-jem {Id} nije pronađen.", id);
                throw new NotFoundException($"Izdavač sa ID-jem {id} nije pronađen.");
            }

            return MapToDto(publisher);
        }

        public async Task<List<PublisherDto>> GetSortedAsync(PublisherSortType sortType)
        {
            var publishers = await _publishersRepo.GetAllPublishersAsync();

            return sortType switch
            {
                PublisherSortType.NameAsc => publishers.OrderBy(p => p.Name).Select(MapToDto).ToList(),
                PublisherSortType.NameDesc => publishers.OrderByDescending(p => p.Name).Select(MapToDto).ToList(),
                PublisherSortType.AddressAsc => publishers.OrderBy(p => p.Address).Select(MapToDto).ToList(),
                PublisherSortType.AddressDesc => publishers.OrderByDescending(p => p.Address).Select(MapToDto).ToList(),
                _ => publishers.OrderBy(p => p.Name).Select(MapToDto).ToList()
            };
        }


        public async Task<PublisherDto> CreateAsync(PublisherDto dto)
        {
            _logger.LogInformation("Kreiranje novog izdavača: {Name}", dto.Name);
            var publisher = new Publisher
            {
                Name = dto.Name,
                Address = dto.Address,
                Website = dto.Website
            };

            await _publishersRepo.AddPublisherAsync(publisher);
            _logger.LogInformation("Izdavač uspešno kreiran: {Name}", publisher.Name);
            return MapToDto(publisher);
        }

        public async Task<PublisherDto> UpdateAsync(int id, PublisherDto dto)
        {
            _logger.LogInformation("Ažuriranje izdavača ID={Id}", id);
            var existing = await _publishersRepo.GetPublisherAsync(id);
            if (existing == null)
            {
                _logger.LogWarning("Izdavač sa ID-jem {Id} nije pronađen za ažuriranje.", id);
                throw new NotFoundException($"Izdavač sa ID-jem {id} nije pronađen.");
            }

            existing.Name = dto.Name;
            existing.Address = dto.Address;
            existing.Website = dto.Website;

            await _publishersRepo.UpdatePublisherAsync(existing);
            _logger.LogInformation("Izdavač ID={Id} uspešno ažuriran.", id);
            return MapToDto(existing);
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

        private PublisherDto MapToDto(Publisher publisher)
        {
            return new PublisherDto
            {
                Id = (int)publisher.Id,
                Name = publisher.Name,
                Address = publisher.Address,
                Website = publisher.Website
            };
        }
    }
}
