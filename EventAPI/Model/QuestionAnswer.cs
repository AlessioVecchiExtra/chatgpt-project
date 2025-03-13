namespace EventAPI.Models
{
    public class QuestionAnswer
    {
        public int Id { get; set; } // Chiave primaria
        public int QuestionId { get; set; }
        public required string AnswerText { get; set; }
        public int Priority { get; set; }
        public Question Question { get; set; } = new();
        public bool Deleted { get; set; }
    }

}