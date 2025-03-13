using EventAPI.Data;
using EventAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventAPI.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly AppDbContext _context;
        public QuestionRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Question?> GetById(int Id)
        {
            return await _context.Questions.FirstOrDefaultAsync(v => v.Id == Id);
        }
    }
}
