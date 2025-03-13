using System.Collections.Generic;
using EventAPI.Models;

namespace EventAPI.Repositories
{
    public interface IQuestionRepository
    {
        Task<Question?> GetById(int Id);
    }
}
