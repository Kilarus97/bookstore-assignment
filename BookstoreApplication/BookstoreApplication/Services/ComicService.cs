using System.Text.Json;
using AutoMapper;
using BookstoreApplication.Data;
using BookstoreApplication.Interfaces;
using BookstoreApplication.Models;
using Google;
using Microsoft.EntityFrameworkCore;

public class ComicService : IComicService
{
    private readonly IComicVineConnection _connection;
    private readonly IConfiguration _config;
    private readonly BookstoreDbContext _context;
    private readonly IMapper _mapper;

    public ComicService(IComicVineConnection connection, IConfiguration config, BookstoreDbContext context , IMapper mapper)
    {
        _connection = connection;
        _config = config;
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<VolumeDto>> SearchVolumes(string name)
    {
        var baseUrl = _config["ComicVineBaseUrl"];
        var apiKey = _config["ComicVineAPIKey"];
        var url = $"{baseUrl}/volumes/?api_key={apiKey}&format=json&filter=name:{name}";

        var json = await _connection.Get(url);
        Console.WriteLine(json);
        var volumes = JsonSerializer.Deserialize<List<VolumeDto>>(json);
        return volumes;
    }

    public async Task<VolumeDto?> SearchVolumeById(int externalVolumeId)
    {
        var baseUrl = _config["ComicVineBaseUrl"];
        var apiKey = _config["ComicVineAPIKey"];
        var url = $"{baseUrl}/volume/4050-{externalVolumeId}/?api_key={apiKey}&format=json";

        var json = await _connection.Get(url);
        Console.WriteLine(json);

        var volume = JsonSerializer.Deserialize<VolumeDto>(json);
        return volume;
    }


    public async Task<int> CreateIssueFromExternalAsync(CreateIssueDto dto)
    {
        var baseUrl = _config["ComicVineBaseUrl"];
        var apiKey = _config["ComicVineAPIKey"];
        var url = $"{baseUrl}/issue/4000-{dto.ExternalId}/?api_key={apiKey}&format=json";

        var json = await _connection.Get(url);
        var issueDto = JsonSerializer.Deserialize<IssueDto>(json);

        if (issueDto == null)
            throw new Exception("Issue not found in external API");

        // ✅ Proveri da li već postoji
        var existingIssue = await _context.Issues
            .FirstOrDefaultAsync(i => i.ExternalId == dto.ExternalId);

        if (existingIssue != null)
        {
            // Ako postoji, možeš da ažuriraš podatke ili samo vratiš Id
            existingIssue.Price = dto.Price;
            existingIssue.AvailableCopies = dto.AvailableCopies;
            await _context.SaveChangesAsync();
            return existingIssue.Id;
        }

        // Mapiraj osnovne podatke iz IssueDto
        var issue = _mapper.Map<Issue>(issueDto);
        issue.ExternalId = dto.ExternalId;
        issue.Price = dto.Price;
        issue.AvailableCopies = dto.AvailableCopies;
        issue.CreatedAt = DateTime.UtcNow;

        // Proveri da li volume već postoji
        var existingVolume = await _context.Volume
            .FirstOrDefaultAsync(v => v.ExternalId == issueDto.Volume.Id);

        if (existingVolume == null)
        {
            var volumeDto = await SearchVolumeById(issueDto.Volume.Id);
            var volume = _mapper.Map<Volume>(volumeDto);
            volume.ExternalId = issueDto.Volume.Id;

            var publisher = await _context.Publishers
                .FirstOrDefaultAsync(p => p.Name == volumeDto.Publisher.Name);

            if (publisher == null)
            {
                publisher = _mapper.Map<Publisher>(volumeDto.Publisher);
                _context.Publishers.Add(publisher);
                await _context.SaveChangesAsync();
            }

            volume.PublisherId = (int)publisher.Id;
            _context.Volume.Add(volume);
            await _context.SaveChangesAsync();

            issue.VolumeId = volume.Id;
        }
        else
        {
            issue.VolumeId = existingVolume.Id;
        }

        _context.Issues.Add(issue);
        await _context.SaveChangesAsync();
        return issue.Id;
    }


    public async Task<List<IssueDto>> GetAllLocalIssuesAsync()
    {
        var issues = await _context.Issues
            .Include(i => i.Volume)
            .Include(i => i.Volume.Publisher)
            .ToListAsync();
        return _mapper.Map<List<IssueDto>>(issues);
    }




    public async Task<List<IssueDto>> GetIssues(int volumeId)
    {
        var baseUrl = _config["ComicVineBaseUrl"];
        var apiKey = _config["ComicVineAPIKey"];
        var url = $"{baseUrl}/issues/?api_key={apiKey}&format=json&filter=volume:{volumeId}";

        var json = await _connection.Get(url);
        var issues = JsonSerializer.Deserialize<List<IssueDto>>(json);
        return issues;
    }



    public async Task<IssueDto?> GetIssueByIdAsync(int id)
    {
        var issue = await _context.Issues
            .Include(i => i.Volume)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (issue == null) return null;

        return new IssueDto
        {
            Id = issue.Id,
            Name = issue.Name,
            IssueNumber = issue.IssueNumber,
            CoverDate = issue.CoverDate,
            Description = issue.Description,
            Image = new ImageDto { OriginalUrl = issue.ImageUrl },
            Volume = new VolumeDto
            {
                Id = issue.Volume.ExternalId,
                Name = issue.Volume.Name
            }
        };
    }
}
