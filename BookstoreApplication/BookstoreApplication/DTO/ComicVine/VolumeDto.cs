using System.Text.Json.Serialization;
using BookstoreApplication.DTO;

public class VolumeDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("aliases")]
    public string Aliases { get; set; }

    [JsonPropertyName("count_of_issues")]
    public int CountOfIssues { get; set; }

    [JsonPropertyName("start_year")]
    public string StartYear { get; set; }

    [JsonPropertyName("deck")]
    public string Description { get; set; }

    [JsonPropertyName("publisher")]
    public PublisherDto Publisher { get; set; }
}
