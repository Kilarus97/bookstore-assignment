using BookstoreApplication.DTO;
using BookstoreApplication.Interfaces;
using BookstoreApplication.Models;
using BookstoreApplication.Repository;

namespace BookstoreApplication.Services
{
    public class BookServices : IBookServices
    {
        private readonly IBooksRepo _booksRepo;
        private readonly IAuthorsRepo _authorsRepo;
        private readonly IPublishersRepo _publishersRepo;

        public BookServices(IBooksRepo booksRepo, IAuthorsRepo authorsRepo, IPublishersRepo publishersRepo)
        {
            _booksRepo = booksRepo;
            _authorsRepo = authorsRepo;
            _publishersRepo = publishersRepo;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            return await _booksRepo.GetAllBooksAsync();
        }

        public async Task<Book?> GetBookAsync(int id)
        {
            return await _booksRepo.GetBookAsync(id);
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            var author = await _authorsRepo.GetAuthorAsync(book.AuthorId);
            if (author == null) throw new Exception("Author not found");

            var publisher = await _publishersRepo.GetPublisherAsync(book.PublisherId);
            if (publisher == null) throw new Exception("Publisher not found");

            book.Author = author;
            book.Publisher = publisher;

            await _booksRepo.AddBookAsync(book);
            return book;
        }

        public async Task<Book> UpdateBookAsync(int id, Book book)
        {
            var existingBook = await _booksRepo.GetBookAsync(id);
            if (existingBook == null) throw new Exception("Book not found");

            var author = await _authorsRepo.GetAuthorAsync(book.AuthorId);
            if (author == null) throw new Exception("Author not found");

            var publisher = await _publishersRepo.GetPublisherAsync(book.PublisherId);
            if (publisher == null) throw new Exception("Publisher not found");

            existingBook.Title = book.Title;
            existingBook.PageCount = book.PageCount;
            existingBook.PublishedDate = book.PublishedDate;
            existingBook.ISBN = book.ISBN;
            existingBook.Author = author;
            existingBook.AuthorId = author.Id;
            existingBook.Publisher = publisher;
            existingBook.PublisherId = publisher.Id;

            await _booksRepo.UpdateBookAsync(existingBook);
            return existingBook;
        }

        public async Task DeleteBookAsync(int id)
        {
            await _booksRepo.DeleteBookAsync(id);
        }
    }
}
