namespace EventAPI.Models
{
    public class Question
    {
        public int Id { get; set; } // Chiave primaria
        public int MeetingId { get; set; }
        //public int SessionNumber { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public int Priority { get; set; }
        public List<QuestionAnswer> QuestionAnswers { get; set; } = new List<QuestionAnswer>();
        public bool Deleted { get; set; }
    }

}