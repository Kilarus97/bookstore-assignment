using System.Text.Json;
using AutoMapper;
using BookstoreApplication.Interfaces;
using BookstoreApplication.Models;

public class ComicService : IComicService
{
    private readonly IComicVineConnection _connection;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;
    private readonly IComicRepository _comicRepo;

    public ComicService(
        IComicVineConnection connection,
        IConfiguration config,
        IMapper mapper,
        IComicRepository comicRepo)
    {
        _connection = connection;
        _config = config;
        _mapper = mapper;
        _comicRepo = comicRepo;
    }

    // ✅ ostaje isto ime
    public async Task<List<VolumeDto>> SearchVolumes(string name)
    {
        var baseUrl = _config["ComicVineBaseUrl"];
        var apiKey = _config["ComicVineAPIKey"];
        var url = $"{baseUrl}/volumes/?api_key={apiKey}&format=json&filter=name:{name}";

        var json = await _connection.Get(url);
        var volumes = JsonSerializer.Deserialize<List<VolumeDto>>(json);
        return volumes ?? new List<VolumeDto>();
    }

    // ✅ ostaje isto ime
    public async Task<VolumeDto?> SearchVolumeById(int externalVolumeId)
    {
        var baseUrl = _config["ComicVineBaseUrl"];
        var apiKey = _config["ComicVineAPIKey"];
        var url = $"{baseUrl}/volume/4050-{externalVolumeId}/?api_key={apiKey}&format=json";

        var json = await _connection.Get(url);
        var volume = JsonSerializer.Deserialize<VolumeDto>(json);
        return volume;
    }

    // ✅ ostaje isto ime
    public async Task<int> CreateIssueFromExternalAsync(CreateIssueDto dto)
    {
        var baseUrl = _config["ComicVineBaseUrl"];
        var apiKey = _config["ComicVineAPIKey"];
        var url = $"{baseUrl}/issue/4000-{dto.ExternalId}/?api_key={apiKey}&format=json";

        var json = await _connection.Get(url);
        var issueDto = JsonSerializer.Deserialize<IssueDto>(json);

        if (issueDto == null)
            throw new Exception("Issue not found in external API");

        // Mapiraj IssueDto u ComicDocument
        var comicDoc = new ComicDocument
        {
            ComicVineId = issueDto.Id,
            Title = issueDto.Name ?? "Unknown",
            Description = issueDto.Description,
            IssueNumber = issueDto.IssueNumber,
            ImageUrl = issueDto.Image?.OriginalUrl,
            FetchedAt = DateTime.UtcNow
        };

        if (DateTime.TryParse(issueDto.CoverDate, out var pd))
            comicDoc.PublishDate = pd;

        // Upsert u Mongo
        await _comicRepo.UpsertAsync(comicDoc);

        // Vraćamo external id (jer nemamo SQL Id)
        return dto.ExternalId;
    }

    // ✅ ostaje isto ime
    public async Task<List<IssueDto>> GetAllLocalIssuesAsync()
    {
        var docs = await _comicRepo.GetAllAsync(1000);
        return docs.Select(d => new IssueDto
        {
            Id = (int)d.ComicVineId,
            Name = d.Title,
            IssueNumber = d.IssueNumber,
            CoverDate = d.PublishDate?.ToString("yyyy-MM-dd"),
            Description = d.Description,
            Image = new ImageDto { OriginalUrl = d.ImageUrl },
            Volume = new VolumeDto { Id = 0, Name = "" } // prilagodi ako želiš volume podatke
        }).ToList();
    }

    // ✅ ostaje isto ime
    public async Task<List<IssueDto>> GetIssues(int volumeId)
    {
        var baseUrl = _config["ComicVineBaseUrl"];
        var apiKey = _config["ComicVineAPIKey"];
        var url = $"{baseUrl}/issues/?api_key={apiKey}&format=json&filter=volume:{volumeId}";

        var json = await _connection.Get(url);
        var issues = JsonSerializer.Deserialize<List<IssueDto>>(json);
        return issues ?? new List<IssueDto>();
    }

    // ✅ ostaje isto ime
    public async Task<IssueDto?> GetIssueByIdAsync(int id)
    {
        var doc = await _comicRepo.GetByComicVineIdAsync(id);
        if (doc == null) return null;

        return new IssueDto
        {
            Id = (int)doc.ComicVineId,
            Name = doc.Title,
            IssueNumber = doc.IssueNumber,
            CoverDate = doc.PublishDate?.ToString("yyyy-MM-dd"),
            Description = doc.Description,
            Image = new ImageDto { OriginalUrl = doc.ImageUrl },
            Volume = new VolumeDto { Id = 0, Name = "" }
        };
    }
}
