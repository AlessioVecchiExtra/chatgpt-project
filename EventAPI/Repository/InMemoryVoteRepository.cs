using EventAPI.Models;

namespace EventAPI.Repositories
{
    public class InMemoryVoteRepository : IVoteRepository
    {
        private readonly List<Vote> _votes = new();

        public Task AddVoteAsync(Vote vote)
        {
            _votes.Add(vote);
            return Task.CompletedTask;
        }

        public Task<List<Vote>> GetAllVotesAsync()
        {
            return Task.FromResult(_votes.ToList());
        }

        public Task<List<Vote>> GetVotesBySessionAsync(int sessionId)
        {
            var result = _votes.Where(v => v.SessionId == sessionId).ToList();
            return Task.FromResult(result);
        }
    }
}
