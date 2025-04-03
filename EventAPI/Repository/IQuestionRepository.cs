using System.Collections.Generic;
using EventAPI.Models;

namespace EventAPI.Repositories
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<Question>> GetByMeetingId(int Id);
        Task<Question?> GetById(int Id);
        Task<int> GetByMeetingIdCount(int meetingId);
    }
}
