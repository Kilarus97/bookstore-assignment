using AutoMapper;
using BookstoreApplication.DTO;
using BookstoreApplication.Enums;
using BookstoreApplication.Exceptions;
using BookstoreApplication.Interfaces;
using BookstoreApplication.Models;
using Microsoft.Extensions.Logging;

namespace BookstoreApplication.Services
{
    public class BookServices : IBookServices
    {
        private readonly IBooksRepo _booksRepo;
        private readonly IAuthorsRepo _authorsRepo;
        private readonly IPublishersRepo _publishersRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<BookServices> _logger;

        public BookServices(
            IBooksRepo booksRepo,
            IAuthorsRepo authorsRepo,
            IPublishersRepo publishersRepo,
            IMapper mapper,
            ILogger<BookServices> logger)
        {
            _booksRepo = booksRepo;
            _authorsRepo = authorsRepo;
            _publishersRepo = publishersRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<BookDetailsDto>> SearchBookDetailsAsync(BookSearchDto search)
        {
            try
            {
                _logger.LogInformation("Kombinovana pretraga započeta: {@Search}", search);
                var result = await _booksRepo.SearchBookDetailsAsync(search);
                _logger.LogInformation("Pretraga završena. Pronađeno {Count} knjiga.", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Greška tokom kombinovane pretrage.");
                throw;
            }
        }



        public async Task<List<BookDetailsDto>> GetSortedDetailsAsync(BookSortType sortType)
        {
            _logger.LogInformation("Započinjem sortiranje knjiga po tipu: {SortType}", sortType);

            var books = await _booksRepo.GetAllBookDetailsAsync();

            var sorted = sortType switch
            {
                BookSortType.TitleAsc => books.OrderBy(b => b.Title),
                BookSortType.TitleDesc => books.OrderByDescending(b => b.Title),
                BookSortType.PublishDateAsc => books.OrderBy(b => b.PublishedDate),
                BookSortType.PublishDateDesc => books.OrderByDescending(b => b.PublishedDate),
                BookSortType.AuthorNameAsc => books.OrderBy(b => b.AuthorFullName),
                BookSortType.AuthorNameDesc => books.OrderByDescending(b => b.AuthorFullName),
                _ => books.OrderBy(b => b.Title)
            };

            _logger.LogInformation("Sortiranje završeno. Vraćam {Count} knjiga.", sorted.Count());

            return _mapper.Map<List<BookDetailsDto>>(sorted.ToList());
        }





        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            _logger.LogInformation("Dohvatanje svih knjiga iz baze.");
            var books = await _booksRepo.GetAllBooksAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDetailsDto?> GetBookDetailsAsync(int id)
        {
            _logger.LogInformation("Dohvatanje detalja za knjigu ID={Id}", id);
            var book = await _booksRepo.GetBookAsync(id);
            if (book == null)
            {
                _logger.LogWarning("Knjiga sa ID-jem {Id} nije pronađena.", id);
                throw new NotFoundException($"Knjiga sa ID-jem {id} nije pronađena.");
            }

            return _mapper.Map<BookDetailsDto>(book);
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            _logger.LogInformation("Kreiranje nove knjige: {Title}, AutorID={AuthorId}, PublisherID={PublisherId}",
                book.Title, book.AuthorId, book.PublisherId);

            var author = await _authorsRepo.GetAuthorAsync(book.AuthorId);
            if (author == null)
            {
                _logger.LogWarning("Autor sa ID-jem {AuthorId} nije pronađen.", book.AuthorId);
                throw new NotFoundException($"Autor sa ID-jem {book.AuthorId} nije pronađen.");
            }

            var publisher = await _publishersRepo.GetPublisherAsync(book.PublisherId);
            if (publisher == null)
            {
                _logger.LogWarning("Izdavač sa ID-jem {PublisherId} nije pronađen.", book.PublisherId);
                throw new NotFoundException($"Izdavač sa ID-jem {book.PublisherId} nije pronađen.");
            }

            book.Author = author;
            book.Publisher = publisher;

            await _booksRepo.AddBookAsync(book);
            _logger.LogInformation("Knjiga uspešno kreirana: {Title}", book.Title);
            return book;
        }

        public async Task<Book> UpdateBookAsync(int id, Book book)
        {
            _logger.LogInformation("Ažuriranje knjige ID={Id}: {Title}", id, book.Title);

            var existingBook = await _booksRepo.GetBookAsync(id);
            if (existingBook == null)
            {
                _logger.LogWarning("Knjiga sa ID-jem {Id} nije pronađena za ažuriranje.", id);
                throw new NotFoundException($"Knjiga sa ID-jem {id} nije pronađena.");
            }

            var author = await _authorsRepo.GetAuthorAsync(book.AuthorId);
            if (author == null)
            {
                _logger.LogWarning("Autor sa ID-jem {AuthorId} nije pronađen.", book.AuthorId);
                throw new NotFoundException($"Autor sa ID-jem {book.AuthorId} nije pronađen.");
            }

            var publisher = await _publishersRepo.GetPublisherAsync(book.PublisherId);
            if (publisher == null)
            {
                _logger.LogWarning("Izdavač sa ID-jem {PublisherId} nije pronađen.", book.PublisherId);
                throw new NotFoundException($"Izdavač sa ID-jem {book.PublisherId} nije pronađen.");
            }

            existingBook.Title = book.Title;
            existingBook.PageCount = book.PageCount;
            existingBook.PublishedDate = book.PublishedDate;
            existingBook.ISBN = book.ISBN;
            existingBook.Author = author;
            existingBook.AuthorId = author.Id;
            existingBook.Publisher = publisher;
            existingBook.PublisherId = publisher.Id;

            await _booksRepo.UpdateBookAsync(existingBook);
            _logger.LogInformation("Knjiga ID={Id} uspešno ažurirana.", id);
            return existingBook;
        }

        public async Task DeleteBookAsync(int id)
        {
            _logger.LogInformation("Brisanje knjige ID={Id}", id);
            var book = await _booksRepo.GetBookAsync(id);
            if (book == null)
            {
                _logger.LogWarning("Knjiga sa ID-jem {Id} ne postoji za brisanje.", id);
                throw new NotFoundException($"Knjiga sa ID-jem {id} ne postoji.");
            }

            await _booksRepo.DeleteBookAsync(id);
            _logger.LogInformation("Knjiga ID={Id} uspešno obrisana.", id);
        }
    }
}
