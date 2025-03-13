namespace EventAPI.Models
{
    public class VoteDto
    {
        public int QuestionId { get; set; }
        public int? QuestionAnswerId { get; set; }
        public string? QuestionAnswerText { get; set; }
    }
}
