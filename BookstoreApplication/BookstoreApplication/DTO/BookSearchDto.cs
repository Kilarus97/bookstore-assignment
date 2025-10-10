namespace BookstoreApplication.DTO
{
    public class BookSearchDto
    {
        public string? TitleContains { get; set; }
        public DateTime? PublishedFrom { get; set; }
        public DateTime? PublishedTo { get; set; }
        public int? AuthorId { get; set; }
        public string? AuthorNameContains { get; set; }
        public DateTime? AuthorBornFrom { get; set; }
        public DateTime? AuthorBornTo { get; set; }
        public string? SortType { get; set; }
    }
}
