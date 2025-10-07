using AutoMapper;
using BookstoreApplication.DTO;
using BookstoreApplication.Exceptions;
using BookstoreApplication.Interfaces;
using BookstoreApplication.Models;


namespace BookstoreApplication.Services
{
    public class BookServices : IBookServices
    {
        private readonly IBooksRepo _booksRepo;
        private readonly IAuthorsRepo _authorsRepo;
        private readonly IPublishersRepo _publishersRepo;
        private readonly IMapper _mapper;

        public BookServices(
            IBooksRepo booksRepo,
            IAuthorsRepo authorsRepo,
            IPublishersRepo publishersRepo,
            IMapper mapper)
        {
            _booksRepo = booksRepo;
            _authorsRepo = authorsRepo;
            _publishersRepo = publishersRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _booksRepo.GetAllBooksAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDetailsDto?> GetBookDetailsAsync(int id)
        {
            var book = await _booksRepo.GetBookAsync(id);
            if (book == null)
                throw new NotFoundException($"Knjiga sa ID-jem {id} nije pronađena.");

            return _mapper.Map<BookDetailsDto>(book);
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            var author = await _authorsRepo.GetAuthorAsync(book.AuthorId);
            if (author == null)
                throw new NotFoundException($"Autor sa ID-jem {book.AuthorId} nije pronađen.");

            var publisher = await _publishersRepo.GetPublisherAsync(book.PublisherId);
            if (publisher == null)
                throw new NotFoundException($"Izdavač sa ID-jem {book.PublisherId} nije pronađen.");

            book.Author = author;
            book.Publisher = publisher;

            await _booksRepo.AddBookAsync(book);
            return book;
        }

        public async Task<Book> UpdateBookAsync(int id, Book book)
        {
            var existingBook = await _booksRepo.GetBookAsync(id);
            if (existingBook == null)
                throw new NotFoundException($"Knjiga sa ID-jem {id} nije pronađena.");

            var author = await _authorsRepo.GetAuthorAsync(book.AuthorId);
            if (author == null)
                throw new NotFoundException($"Autor sa ID-jem {book.AuthorId} nije pronađen.");

            var publisher = await _publishersRepo.GetPublisherAsync(book.PublisherId);
            if (publisher == null)
                throw new NotFoundException($"Izdavač sa ID-jem {book.PublisherId} nije pronađen.");

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
            var book = await _booksRepo.GetBookAsync(id);
            if (book == null)
                throw new NotFoundException($"Knjiga sa ID-jem {id} ne postoji.");

            await _booksRepo.DeleteBookAsync(id);
        }
    }
}
