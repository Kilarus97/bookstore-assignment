using BookstoreApplication.Models;

public class Review
{
    public int Id { get; set; }
    public string Username { get; set; }
    public int BookId { get; set; }
    public int Rating { get; set; } // 1–5
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Book Book { get; set; }
}
