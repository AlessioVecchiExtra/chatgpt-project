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

        [HttpGet("{questionId}")]
        public async Task<ActionResult<QuestionDto>> GetQuestion(int questionId)
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
