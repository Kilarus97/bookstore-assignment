using BookstoreApplication.Models;

namespace BookstoreApplication.Interfaces
{
    public interface IPublishersRepo
    {
        Task<IEnumerable<Publisher>> GetAllPublishersAsync();
        Task<Publisher?> GetPublisherAsync(int id);
        Task AddPublisherAsync(Publisher publisher);
        Task UpdatePublisherAsync(Publisher publisher);
        Task DeletePublisherAsync(int id);
    }
}
