using BookstoreApplication.Data;
using BookstoreApplication.DTO;
using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repository
{
    public class AuthorsRepo
    {
        private BookstoreDbContext _context;

        public AuthorsRepo(BookstoreDbContext context)
        {
            _context = context;
        }

        // Implement CRUD operations for Author entity here
        public IEnumerable<AuthorDto> GetAllAuthorDtos()
        {
            return _context.Authors
                .Include(a => a.AuthorAwards)
                    .ThenInclude(aa => aa.Award)
                .Select(a => new AuthorDto
                {
                    Id = a.Id,
                    FullName = a.FullName,
                    Awards = a.AuthorAwards.Select(aa => new AwardDto
                    {
                        Name = aa.Award.Name,
                        YearReceived = aa.YearReceived
                    }).ToList()
                })
                .ToList();
        }

        public Author GetAuthor(int id)
        {
            return _context.Authors.Find(id);
        }

        public void AddAuthor(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
        }


        public void UpdateAuthor(Author author)
        {
            _context.Authors.Update(author);
            _context.SaveChanges();
        }

        public void DeleteAuthor(int id)
        {
            var author = _context.Authors.Find(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                _context.SaveChanges();
            }
        } 

        
    }
}
