namespace EventAPI.Dto
{
    public class QuestionDto
    {
        public int Id { get; set; }               
        public string QuestionText { get; set; } = string.Empty;
        public int Priority { get; set; }
        public List<QuestionAnswerDto> QuestionAnswers { get; set; } = new List<QuestionAnswerDto>();        
    }

}