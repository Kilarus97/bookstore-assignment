using System.Text.Json.Serialization;

namespace BookstoreApplication.DTO
{
    public class PublisherDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        public string? Address { get; set; }
        [JsonPropertyName("site_detail_url")]
        public string? Website { get; set; }

        [JsonPropertyName("api_detail_url")]
        public string ApiDetailUrl { get; set; }

    }
}
