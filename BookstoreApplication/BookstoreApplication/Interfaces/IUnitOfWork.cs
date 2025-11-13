using BookstoreApplication.Data;

namespace BookstoreApplication.Interfaces
{
    public interface IUnitOfWork
    {
        public IAuthorsRepo Authors { get; set; }
        public IBooksRepo Books { get; set; }
        public IPublishersRepo Publishers { get; set; }
        public IReviewRepository Review { get; set; }
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task<int> CompleteAsync();
        void Dispose();
    }
}
