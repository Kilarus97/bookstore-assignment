using BookstoreApplication.Data;
using BookstoreApplication.Interfaces;
using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repository
{
    public class PublishersRepo : IPublishersRepo
    {
        private readonly BookstoreDbContext _context;

        public PublishersRepo(BookstoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Publisher>> GetAllPublishersAsync()
        {
            return await _context.Publishers.ToListAsync();
        }

        public async Task<Publisher?> GetPublisherAsync(int id)
        {
            return await _context.Publishers.FindAsync(id);
        }

        public async Task AddPublisherAsync(Publisher publisher)
        {
            await _context.Publishers.AddAsync(publisher);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePublisherAsync(Publisher publisher)
        {
            _context.Publishers.Update(publisher);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePublisherAsync(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);
                await _context.SaveChangesAsync();
            }
        }
    }
}
