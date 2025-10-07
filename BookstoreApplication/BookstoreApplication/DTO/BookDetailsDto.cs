namespace BookstoreApplication.DTO
{
    public class BookDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int PageCount { get; set; }
        public DateTime PublishedDate { get; set; }
        public string ISBN { get; set; } = string.Empty;

        public int AuthorId { get; set; }
        public string AuthorFullName { get; set; } = string.Empty;

        public int PublisherId { get; set; }
        public string PublisherName { get; set; } = string.Empty;
    }
}
