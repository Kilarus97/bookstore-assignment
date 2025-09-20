namespace BookstoreApplication.DTO
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public int PageCount { get; set; }
        public DateTime PublishedDate { get; set; }
        public string ISBN { get; set; } = default!;
        public string Author { get; set; } = default!;
        public string Publisher { get; set; } = default!;
        public string Website { get; set; } = default!;
    }
}
