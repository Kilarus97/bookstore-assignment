using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public interface IComicRepository
{
    Task UpsertAsync(ComicDocument comic, CancellationToken ct = default);
    Task BulkUpsertAsync(IEnumerable<ComicDocument> comics, CancellationToken ct = default);
    Task<ComicDocument?> GetByComicVineIdAsync(long comicVineId, CancellationToken ct = default);
    Task<IEnumerable<ComicDocument>> SearchByTitleAsync(string title, int limit = 50, CancellationToken ct = default);
    Task<IEnumerable<ComicDocument>> GetAllAsync(int limit = 100, CancellationToken ct = default);
    Task DeleteByComicVineIdAsync(long comicVineId, CancellationToken ct = default);
}
