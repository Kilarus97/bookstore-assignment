using BookstoreApplication.Exceptions;
using BookstoreApplication.Interfaces;
using BookstoreApplication.Models;


namespace BookstoreApplication.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublishersRepo _publishersRepo;

        public PublisherService(IPublishersRepo publishersRepo)
        {
            _publishersRepo = publishersRepo;
        }

        public async Task<IEnumerable<Publisher>> GetAllAsync()
        {
            return await _publishersRepo.GetAllPublishersAsync();
        }

        public async Task<Publisher> GetOneAsync(int id)
        {
            var publisher = await _publishersRepo.GetPublisherAsync(id);
            if (publisher == null)
                throw new NotFoundException($"Izdavač sa ID-jem {id} nije pronađen.");

            return publisher;
        }

        public async Task<Publisher> CreateAsync(Publisher publisher)
        {
            await _publishersRepo.AddPublisherAsync(publisher);
            return publisher;
        }

        public async Task<Publisher> UpdateAsync(int id, Publisher publisher)
        {
            var existing = await _publishersRepo.GetPublisherAsync(id);
            if (existing == null)
                throw new NotFoundException($"Izdavač sa ID-jem {id} nije pronađen.");

            existing.Name = publisher.Name;
            existing.Website = publisher.Website;
            await _publishersRepo.UpdatePublisherAsync(existing);
            return existing;
        }

        public async Task DeleteAsync(int id)
        {
            var publisher = await _publishersRepo.GetPublisherAsync(id);
            if (publisher == null)
                throw new NotFoundException($"Izdavač sa ID-jem {id} ne postoji.");

            await _publishersRepo.DeletePublisherAsync(id);
        }
    }
}
