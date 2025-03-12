using System.Collections.Generic;
using System.Linq;
using EventAPI.Models;

namespace EventAPI.Repositories
{
    public class InMemoryQuestionRepository : IQuestionRepository
    {
        private readonly List<Question> _questions = new List<Question>
        {
            new Question
            {
                SessionId = 1,
                QuestionText = "Quale parola rappresenta meglio la Sessione 1?",
                Terms = new List<string> { "Innovazione", "Collaborazione", "Creatività", "Ispirazione", "Flessibilità", "Cambiamento", "Coinvolgimento", "Efficienza", "Qualità", "Eccellenza" }
            },
            new Question
            {
                SessionId = 2,
                QuestionText = "Quale parola rappresenta meglio la Sessione 2?",
                Terms = new List<string> { "Leadership", "Strategia", "Visione", "Comunicazione", "Integrità", "Passione", "Determinazione", "Empatia", "Resilienza", "Trasparenza" }
            },
            new Question
            {
                SessionId = 3,
                QuestionText = "Quale parola rappresenta meglio la Sessione 3?",
                Terms = new List<string> { "Innovazione", "Collaborazione", "Creatività", "Ispirazione", "Flessibilità", "Cambiamento", "Coinvolgimento", "Efficienza", "Qualità", "Eccellenza" }
            }
        };

        public Question? GetQuestionBySessionId(int sessionId)
        {
            return _questions.FirstOrDefault(q => q.SessionId == sessionId);
        }
    }
}
