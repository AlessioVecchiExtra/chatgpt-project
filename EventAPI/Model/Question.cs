namespace EventAPI.Models
{
    public class Question
    {
        public int SessionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public List<string> Terms { get; set; } = new List<string>();
    }
}