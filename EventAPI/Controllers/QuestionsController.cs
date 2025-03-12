using Microsoft.AspNetCore.Mvc;
using EventAPI.Models;
using EventAPI.Repositories;

namespace EventAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionsController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        [HttpGet("{sessionId}")]
        public ActionResult<Question> GetQuestion(int sessionId)
        {
            var question = _questionRepository.GetQuestionBySessionId(sessionId);
            if (question == null)
            {
                return NotFound();
            }
            return Ok(question);
        }
    }
}
