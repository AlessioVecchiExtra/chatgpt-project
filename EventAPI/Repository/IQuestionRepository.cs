using System.Collections.Generic;
using EventAPI.Models;

namespace EventAPI.Repositories
{
    public interface IQuestionRepository
    {
        Question? GetQuestionBySessionId(int sessionId);
    }
}
