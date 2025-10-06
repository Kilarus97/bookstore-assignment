using BookstoreApplication.Repository;
using BookstoreApplication.DTO;
using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public class BookServices
    {
        private readonly BooksRepo _booksRepo;
        private readonly AuthorsRepo _authorRepo;
        private readonly PublishersRepo _publisherRepo;

        public BookServices(BooksRepo booksRepo, AuthorsRepo authorRepo, PublishersRepo publisherRepo)
        {
            _booksRepo = booksRepo;
            _authorRepo = authorRepo;
            _publisherRepo = publisherRepo;
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
            var author = await _authorRepo.GetAuthorAsync(book.AuthorId);
            if (author == null) throw new Exception("Author not found");

            var publisher = await _publisherRepo.GetPublisherAsync(book.PublisherId);
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

            var author = await _authorRepo.GetAuthorAsync(book.AuthorId);
            if (author == null) throw new Exception("Author not found");

            var publisher = await _publisherRepo.GetPublisherAsync(book.PublisherId);
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
