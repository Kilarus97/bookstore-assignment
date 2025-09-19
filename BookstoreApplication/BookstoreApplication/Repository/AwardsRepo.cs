using BookstoreApplication.Data;
using BookstoreApplication.Models;

namespace BookstoreApplication.Repository
{
    public class AwardsRepo
    {
        private BookstoreDbContext _context;

        public AwardsRepo(BookstoreDbContext context)
        {
            _context = context;
        }

        // Implement CRUD operations for Award entity here

        public IEnumerable<Award> GetAllAwards()
        {
            return _context.Awards.ToList();
        }

        public Award GetAward(int id)
        {
            return _context.Awards.Find(id);
        }

        public void AddAward(Award award)
        {
            _context.Awards.Add(award);
            _context.SaveChanges();
        }

        public void UpdateAward(Award award)
        {
            _context.Awards.Update(award);
            _context.SaveChanges();
        }
        public void DeleteAward(int id)
        {
            var award = _context.Awards.Find(id);
            if (award != null)
            {
                _context.Awards.Remove(award);
                _context.SaveChanges();
            }
        }
    }
}
