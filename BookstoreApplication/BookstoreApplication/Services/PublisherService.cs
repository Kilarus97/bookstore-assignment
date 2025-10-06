using BookstoreApplication.Models;
using BookstoreApplication.Repository;

namespace BookstoreApplication.Services
{
    public class PublisherService
    {
        private readonly PublishersRepo _publishersRepo;

        public PublisherService(PublishersRepo publishersRepo)
        {
            _publishersRepo = publishersRepo;
        }

        public async Task<IEnumerable<Publisher>> GetAllAsync()
        {
            return await _publishersRepo.GetAllPublishersAsync();
        }

        public async Task<Publisher?> GetOneAsync(int id)
        {
            return await _publishersRepo.GetPublisherAsync(id);
        }

        public async Task<Publisher> CreateAsync(Publisher publisher)
        {
            await _publishersRepo.AddPublisherAsync(publisher);
            return publisher;
        }

        public async Task<Publisher> UpdateAsync(int id, Publisher publisher)
        {
            var existing = await _publishersRepo.GetPublisherAsync(id);
            if (existing == null) throw new Exception("Publisher not found");

            existing.Name = publisher.Name;
            existing.Website = publisher.Website;
            await _publishersRepo.UpdatePublisherAsync(existing);
            return existing;
        }

        public async Task DeleteAsync(int id)
        {
            await _publishersRepo.DeletePublisherAsync(id);
        }
    }
}
