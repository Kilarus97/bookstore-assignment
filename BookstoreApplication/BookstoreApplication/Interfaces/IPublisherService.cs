using BookstoreApplication.Models;

namespace BookstoreApplication.Interfaces
{
    public interface IPublisherService
    {
        Task<IEnumerable<Publisher>> GetAllAsync();
        Task<Publisher?> GetOneAsync(int id);
        Task<Publisher> CreateAsync(Publisher publisher);
        Task<Publisher> UpdateAsync(int id, Publisher publisher);
        Task DeleteAsync(int id);
    }
}
