namespace BookstoreApplication.Interfaces
{
    public interface IComicService
    {
        public Task<List<VolumeDto>> SearchVolumes(string name);
        public Task<List<IssueDto>> GetIssues(int volumeId);
        Task<int> CreateIssueFromExternalAsync(CreateIssueDto dto);
        Task<IssueDto?> GetIssueByIdAsync(int id);
        Task<List<IssueDto>> GetAllLocalIssuesAsync();
    }
}
