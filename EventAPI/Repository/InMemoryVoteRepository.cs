using EventAPI.Models;

namespace EventAPI.Repositories
{
    public class InMemoryVoteRepository : IVoteRepository
    {
        private readonly List<Vote> _votes = new();

        public Task Add(Vote vote)
        {
            _votes.Add(vote);
            return Task.CompletedTask;
        }

        public async Task Clear(int sessionId)
        {
            _votes.Where(x => x.QuestionId == sessionId)
                .ToList()
                .ForEach(x => _votes.Remove(x));
        }

        public Task<List<Vote>> GetAll(int meetingId)
        {
            return Task.FromResult(_votes.ToList());
        }

        public Task<List<Vote>> GetVotesByQuestionId(int questionId)
        {
            var result = _votes.Where(v => v.QuestionId == questionId).ToList();
            return Task.FromResult(result);
        }
    }
}
