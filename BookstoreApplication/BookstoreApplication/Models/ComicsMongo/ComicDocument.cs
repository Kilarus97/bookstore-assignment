using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class ComicDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    // Originalni Comic Vine id (koristi za idempotentnost/upsert)
    [BsonElement("comicVineId")]
    public long ComicVineId { get; set; }

    [BsonElement("title")]
    public string Title { get; set; } = string.Empty;

    [BsonElement("description")]
    public string? Description { get; set; }

    [BsonElement("issueNumber")]
    public string? IssueNumber { get; set; }

    [BsonElement("publishDate")]
    public DateTime? PublishDate { get; set; }

    [BsonElement("imageUrl")]
    public string? ImageUrl { get; set; }

    [BsonElement("creators")]
    public List<string> Creators { get; set; } = new();

    [BsonElement("fetchedAt")]
    public DateTime FetchedAt { get; set; } = DateTime.UtcNow;
}
