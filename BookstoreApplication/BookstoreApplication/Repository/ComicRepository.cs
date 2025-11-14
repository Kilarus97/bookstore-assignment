using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

public class ComicRepository : IComicRepository
{
    private readonly IMongoCollection<ComicDocument> _coll;

    public ComicRepository(IMongoDatabase db)
    {
        _coll = db.GetCollection<ComicDocument>("comics");

        // Kreiraj jedinstveni indeks na comicVineId (za efikasan upsert)
        var indexKeys = Builders<ComicDocument>.IndexKeys.Ascending(c => c.ComicVineId);
        var indexModel = new CreateIndexModel<ComicDocument>(indexKeys, new CreateIndexOptions { Unique = true, Background = false });
        _coll.Indexes.CreateOne(indexModel);

        // Text index nad title za pretragu po naslovu (opciono)
        var textIndex = Builders<ComicDocument>.IndexKeys.Text(c => c.Title);
        _coll.Indexes.CreateOne(new CreateIndexModel<ComicDocument>(textIndex));
    }

    public async Task UpsertAsync(ComicDocument comic, CancellationToken ct = default)
    {
        // Ako Id nije setovan, generiši novi ObjectId
        if (comic.Id == ObjectId.Empty)
            comic.Id = ObjectId.GenerateNewId();

        comic.FetchedAt = DateTime.UtcNow;

        var filter = Builders<ComicDocument>.Filter.Eq(c => c.ComicVineId, comic.ComicVineId);
        var options = new ReplaceOptions { IsUpsert = true };

        await _coll.ReplaceOneAsync(filter, comic, options, ct);
    }

    public async Task BulkUpsertAsync(IEnumerable<ComicDocument> comics, CancellationToken ct = default)
    {
        var models = new List<WriteModel<ComicDocument>>();
        foreach (var comic in comics)
        {
            var filter = Builders<ComicDocument>.Filter.Eq(c => c.ComicVineId, comic.ComicVineId);
            comic.FetchedAt = DateTime.UtcNow;
            var replace = new ReplaceOneModel<ComicDocument>(filter, comic) { IsUpsert = true };
            models.Add(replace);
        }

        if (models.Count == 0) return;

        // BulkWrite za brz import/upsert; IsOrdered = false dopušta paralelne upise
        await _coll.BulkWriteAsync(models, new BulkWriteOptions { IsOrdered = false }, ct);
    }

    public async Task<ComicDocument?> GetByComicVineIdAsync(long comicVineId, CancellationToken ct = default)
    {
        return await _coll.Find(c => c.ComicVineId == comicVineId).FirstOrDefaultAsync(ct);
    }

    public async Task<IEnumerable<ComicDocument>> SearchByTitleAsync(string title, int limit = 50, CancellationToken ct = default)
    {
        var filter = Builders<ComicDocument>.Filter.Text(title);
        return await _coll.Find(filter).Limit(limit).ToListAsync(ct);
    }

    public async Task<IEnumerable<ComicDocument>> GetAllAsync(int limit = 100, CancellationToken ct = default)
    {
        return await _coll.Find(Builders<ComicDocument>.Filter.Empty).Limit(limit).ToListAsync(ct);
    }

    public async Task DeleteByComicVineIdAsync(long comicVineId, CancellationToken ct = default)
    {
        await _coll.DeleteOneAsync(c => c.ComicVineId == comicVineId, ct);
    }
}
