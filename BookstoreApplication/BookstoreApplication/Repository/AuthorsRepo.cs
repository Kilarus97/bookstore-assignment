using BookstoreApplication.Data;
using BookstoreApplication.DTO;
using BookstoreApplication.Interfaces;
using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repository
{
    public class AuthorsRepo : IAuthorsRepo
    {
        private readonly BookstoreDbContext _context;

        public AuthorsRepo(BookstoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAuthorDtosAsync()
        {
            return await _context.Authors
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
                .ToListAsync();
        }

        public async Task<Author?> GetAuthorAsync(int id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task AddAuthorAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }
        }
    }
}
