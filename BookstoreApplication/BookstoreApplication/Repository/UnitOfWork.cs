using BookstoreApplication.Data;
using BookstoreApplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookstoreApplication.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly BookstoreDbContext _context;
        public IAuthorsRepo Authors { get; set; }
        public IBooksRepo Books { get; set; }
        public IPublishersRepo Publishers { get; set; }
        public IReviewRepository Review { get; set; }

        private IDbContextTransaction _transaction;

        public UnitOfWork(BookstoreDbContext context)
        {
            _context = context;
            Authors = new AuthorsRepo(_context);
            Books = new BooksRepo(_context);
            Publishers = new PublishersRepo(_context);
            Review = new ReviewRepository(_context);
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction == null)
            {
                _transaction = await _context.Database.BeginTransactionAsync();
            }
        }

        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }


        public Task<int> CompleteAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
