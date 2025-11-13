public class ReviewDto
{
    public string Username { get; set; }
    public int BookId { get; set; }
    public int Rating { get; set; } // 1–5
    public string? Comment { get; set; }
}
