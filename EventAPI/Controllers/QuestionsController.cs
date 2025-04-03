using Microsoft.AspNetCore.Mvc;
using EventAPI.Models;
using EventAPI.Repositories;
using AutoMapper;
using EventAPI.Dto;
using System.Threading.Tasks;

namespace EventAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public QuestionsController(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        [HttpGet("{meetingId}")]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetQuestions(int meetingId)
        {
            var questions = await _questionRepository.GetByMeetingId(meetingId);

            var dto = _mapper.Map<IEnumerable<QuestionDto>>(questions);

            return Ok(dto);
        }

        [HttpGet("{meetingId}/count")]
        public async Task<ActionResult<int>> GetQuestionCount(int meetingId)
        {
            var questionsCount = await _questionRepository.GetByMeetingIdCount(meetingId);
             return Ok(questionsCount);
        }

        [HttpGet("{meetingId}/getById/{questionId}")]
        public async Task<ActionResult<QuestionDto>> GetQuestion(int meetingId, int questionId)
        {
            var question = await _questionRepository.GetById(questionId);

            if (question == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<QuestionDto>(question);

            return Ok(dto);
        }
    }
}
