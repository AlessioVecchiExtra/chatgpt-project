namespace EventAPI.Dto
{
    public class MeetingDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }        
    }
}