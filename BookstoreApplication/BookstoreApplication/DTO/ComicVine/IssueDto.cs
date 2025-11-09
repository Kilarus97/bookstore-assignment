using System.Text.Json.Serialization;

public class IssueDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("issue_number")]
    public string IssueNumber { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("cover_date")]
    public string? CoverDate { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("site_detail_url")]
    public string? SiteDetailUrl { get; set; }

    [JsonPropertyName("image")]
    public ImageDto? Image { get; set; }

    [JsonPropertyName("volume")]
    public VolumeDto Volume { get; set; }
}
