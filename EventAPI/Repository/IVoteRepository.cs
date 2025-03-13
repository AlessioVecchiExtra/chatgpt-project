using EventAPI.Models;

namespace EventAPI.Repositories
{
    public interface IVoteRepository
    {
        Task Add(Vote vote);
        Task<List<Vote>> GetAll(int meetingId);        
        Task<List<Vote>> GetVotesByQuestionId(int sessionId);
    }
}
