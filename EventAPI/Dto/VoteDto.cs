namespace EventAPI.Models
{
    public class VoteDto
    {
        public int MeetingId { get; set; }
        public int QuestionId { get; set; }
        public int QuestionAnswerId { get; set; }
        public string QuestionAnswerText { get; set; }
    }
}
