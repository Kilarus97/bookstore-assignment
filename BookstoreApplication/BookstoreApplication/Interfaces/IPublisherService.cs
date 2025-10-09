using BookstoreApplication.DTO;


namespace BookstoreApplication.Interfaces
{
    public interface IPublisherService
    {
        Task<IEnumerable<PublisherDto>> GetAllAsync();
        Task<PublisherDto> GetOneAsync(int id);
        Task<PublisherDto> CreateAsync(PublisherDto publisherDto);
        Task<PublisherDto> UpdateAsync(int id, PublisherDto publisherDto);
        Task<List<PublisherDto>> GetSortedAsync(PublisherSortType sortType);
        Task DeleteAsync(int id);
    }
}
