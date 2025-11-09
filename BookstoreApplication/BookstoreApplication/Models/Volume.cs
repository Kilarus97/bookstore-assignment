namespace BookstoreApplication.Models
{
    public class Volume
    {
        public int Id { get; set; } // lokalni ID u tvojoj bazi

        public int ExternalId { get; set; } // Comic Vine ID (npr. 2870)

        public string Name { get; set; }

        public string Aliases { get; set; }

        public string Description { get; set; }

        public string StartYear { get; set; }

        public string? SiteUrl { get; set; }

        public string? ImageUrl { get; set; }

        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        public ICollection<Issue> Issues { get; set; }
    }

}
