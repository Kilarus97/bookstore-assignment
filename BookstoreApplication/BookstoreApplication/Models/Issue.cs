namespace BookstoreApplication.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CoverDate { get; set; }
        public string IssueNumber { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int ExternalId { get; set; }
        public decimal Price { get; set; }
        public int AvailableCopies { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int VolumeId { get; set; }
        public Volume Volume { get; set; }
    }


}
