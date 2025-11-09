using System.Text.Json.Serialization;

public class ImageDto
{
    [JsonPropertyName("icon_url")]
    public string? IconUrl { get; set; }

    [JsonPropertyName("medium_url")]
    public string? MediumUrl { get; set; }

    [JsonPropertyName("original_url")]
    public string? OriginalUrl { get; set; }
}
