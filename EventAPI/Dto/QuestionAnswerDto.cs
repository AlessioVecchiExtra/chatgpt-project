namespace EventAPI.Dto
{
    public class QuestionAnswerDto
    {
        public int Id { get; set; } // Chiave primaria      
        public required string AnswerText { get; set; }
        public int Priority { get; set; }
    }

}