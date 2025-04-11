using Microsoft.EntityFrameworkCore;
using EventAPI.Data;
using EventAPI.Models;

namespace EventAPI.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private readonly AppDbContext _context;

        public VoteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Vote>> GetAll(int meetingId)
        {
            return await _context.Votes.Where(x => x.Question.MeetingId == meetingId).ToListAsync();
        }

        public async Task<List<Vote>> GetVotesByQuestionId(int questionId)
        {
            return await _context.Votes.Where(v => v.QuestionId == questionId).ToListAsync();
        }

        public async Task Add(Vote vote)
        {
            await _context.Votes.AddAsync(vote);
            await _context.SaveChangesAsync();
        }

        public async Task Clear(int sessionId)
        {
            var toRemove = _context.Votes.Where(x => x.QuestionId == sessionId);
            _context.Votes.RemoveRange(toRemove);
            await _context.SaveChangesAsync();
        }
    }
}
