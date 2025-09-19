using BookstoreApplication.Data;
using BookstoreApplication.Models;

namespace BookstoreApplication.Repository
{
    public class PublishersRepo
    {
        private BookstoreDbContext _context;

        public PublishersRepo(BookstoreDbContext context)
        {
            _context = context;
        }

        // Implement CRUD operations for Publisher entity here
        public IEnumerable<Publisher> GetAllPublishers()
        {
            return _context.Publishers.ToList();
        }

        public Publisher GetPublisher(int id)
        {
            return _context.Publishers.Find(id);
        }

        public void AddPublisher(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            _context.SaveChanges();
        }


        public void UpdatePublisher(Publisher publisher)
        {
            _context.Publishers.Update(publisher);
            _context.SaveChanges();
        }

        public void DeletePublisher(int id)
        {
            var publisher = _context.Publishers.Find(id);
            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);
                _context.SaveChanges();
            }
        }

        
    }
}
