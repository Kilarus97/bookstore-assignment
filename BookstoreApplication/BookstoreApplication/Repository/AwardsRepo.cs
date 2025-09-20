    using BookstoreApplication.Data;
using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repository
{
    public class AwardsRepo
    {
        private readonly BookstoreDbContext _context;

        public AwardsRepo(BookstoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Award>> GetAllAwardsAsync()
        {
            return await _context.Awards.ToListAsync();
        }

        public async Task<Award?> GetAwardAsync(int id)
        {
            return await _context.Awards.FindAsync(id);
        }

        public async Task AddAwardAsync(Award award)
        {
            await _context.Awards.AddAsync(award);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAwardAsync(Award award)
        {
            _context.Awards.Update(award);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAwardAsync(int id)
        {
            var award = await _context.Awards.FindAsync(id);
            if (award != null)
            {
                _context.Awards.Remove(award);
                await _context.SaveChangesAsync();
            }
        }
    }
}
