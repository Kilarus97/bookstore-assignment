public class CreateIssueDto
{
    public int ExternalId { get; set; } // Comic Vine ID
    public int VolumeId { get; set; }   // lokalni ID toma
    public decimal Price { get; set; }
    public int AvailableCopies { get; set; }
}
