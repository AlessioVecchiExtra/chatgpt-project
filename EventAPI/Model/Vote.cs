namespace EventAPI.Models
{
    public class Vote
    {
        public int Id { get; set; } // Chiave primaria
        //public int MeetingId { get; set; }
        public int QuestionId { get; set; }
        //public int SessionNumber { get; set; }
        public int QuestionAnswerId { get; set; }
        public string? QuestionAnswerText { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Question? Question { get; set; }

    }
}
