namespace EventAPI.Models
{
    public class Vote
    {
        public int SessionId { get; set; }
        public string Word { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
