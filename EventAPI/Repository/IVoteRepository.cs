using EventAPI.Models;

namespace EventAPI.Repositories
{
    public interface IVoteRepository
    {
        Task AddVoteAsync(Vote vote);
        Task<List<Vote>> GetAllVotesAsync();
        Task<List<Vote>> GetVotesBySessionAsync(int sessionId);
    }
}
