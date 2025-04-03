using System.Collections.Generic;
using System.Linq;
using EventAPI.Models;

namespace EventAPI.Repositories
{
    public class InMemoryQuestionRepository : IQuestionRepository
    {
        private readonly IEnumerable<Question> _questions = new List<Question>
        {
            new Question
            {
                Id = 1,
                MeetingId = 1,
                QuestionText = "Quale parola rappresenta meglio la Sessione 1?",
                QuestionAnswers = [
                    new() { QuestionId = 1, AnswerText = "Innovazione",  Priority=1,  Deleted = false},
                    new() { QuestionId = 1, AnswerText = "Collaborazione",  Priority=2,  Deleted = false},
                    new() { QuestionId = 1, AnswerText = "Creatività",  Priority=3,  Deleted = false},
                ]
                //{  "Ispirazione", "Flessibilità", "Cambiamento", "Coinvolgimento", "Efficienza", "Qualità", "Eccellenza" }

            },
            new Question
            {
                Id = 2,
                MeetingId = 1,
                QuestionText = "Quale parola rappresenta meglio la Sessione 2?",
                QuestionAnswers = [
                    new() { QuestionId = 1, AnswerText = "Innovazione",  Priority=1,  Deleted = false},
                    new() { QuestionId = 1, AnswerText = "Collaborazione",  Priority=2,  Deleted = false},
                    new() { QuestionId = 1, AnswerText = "Creatività",  Priority=3,  Deleted = false},
                ]
                //Terms = new List<string> { "Leadership", "Strategia", "Visione", "Comunicazione", "Integrità", "Passione", "Determinazione", "Empatia", "Resilienza", "Trasparenza" }
            },
            new Question
            {
                Id = 3,
                MeetingId = 1,
                QuestionText = "Quale parola rappresenta meglio la Sessione 3?",
                QuestionAnswers = [
                    new() { QuestionId = 1, AnswerText = "Innovazione",  Priority=1,  Deleted = false},
                    new() { QuestionId = 1, AnswerText = "Collaborazione",  Priority=2,  Deleted = false},
                    new() { QuestionId = 1, AnswerText = "Creatività",  Priority=3,  Deleted = false},
                ]
                //Terms = new List<string> { "Innovazione 3", "Collaborazione", "Creatività", "Ispirazione", "Flessibilità", "Cambiamento", "Coinvolgimento", "Efficienza", "Qualità", "Eccellenza" }
            }
        };

        public Task<Question?> GetById(int questionId)
        {
            return Task.FromResult(_questions.FirstOrDefault(q => q.Id == questionId));
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
