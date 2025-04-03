// JsonQuestionRepository.cs
using EventAPI.Models;
using System.Text.Json; // o Newtonsoft.Json se preferisci
using System.IO;

namespace EventAPI.Repositories
{
    public class JsonQuestionRepository : IQuestionRepository
    {
        private readonly IEnumerable<Question> _questions = new List<Question>();

        // Esempio: passiamo l'ambiente per costruire il path del file
        public JsonQuestionRepository(IWebHostEnvironment env)
        {
            // Supponendo di avere /Data/questions.json nella root del progetto
            var filePath = Path.Combine(env.ContentRootPath, "Data", "questions.json");

            if (File.Exists(filePath))
            {
                var jsonString = File.ReadAllText(filePath);
                var data = JsonSerializer.Deserialize<List<Question>>(jsonString);
                if (data != null)
                {
                    _questions = data;
                }
            }
        }

        public Task<Question?> GetById(int Id)
        {
            return Task.FromResult(_questions.FirstOrDefault(q => q.Id == Id));
        }

        public Task<IEnumerable<Question>> GetByMeetingId(int meetingId)
        {
            return Task.FromResult(_questions.Where(q => q.MeetingId == meetingId));
        }

        public Task<int> GetByMeetingIdCount(int meetingId)
        {
              return Task.FromResult(_questions.Count(q => q.MeetingId == meetingId));
        }
    }
}
