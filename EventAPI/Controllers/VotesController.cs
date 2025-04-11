using Microsoft.AspNetCore.Mvc;
using EventAPI.Models;
using EventAPI.Repositories;
using AutoMapper;

namespace MyEventApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : ControllerBase
    {
        private readonly IVoteRepository _repository;
        private readonly IMapper _mapper;

        public VotesController(IVoteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // POST: api/Votes
        [HttpPost]
        public async Task<IActionResult> PostVote([FromBody] VoteDto voteDto)
        {
            if (voteDto == null || (voteDto.QuestionAnswerId == null && string.IsNullOrWhiteSpace(voteDto.QuestionAnswerText)))
            {
                return BadRequest("Vote or word is invalid.");
            }

            await _repository.Add(_mapper.Map<Vote>(voteDto));
            return Ok();
        }

        // GET: api/votes/1
        [HttpGet("{meetingId:int}")]
        public async Task<ActionResult<List<VoteDto>>> GetAllVotes(int meetingId)
        {
            var items = await _repository.GetAll(meetingId);
            var votes = _mapper.Map<List<VoteDto?>>(items);

            return Ok(votes);
        }

        // GET: api/votes/question/1
        [HttpGet("question/{questionId:int}")]
        public async Task<ActionResult<List<VoteDto>>> GetVotesBySession(int questionId)
        {
            var votes = _mapper.Map<List<VoteDto?>>(await _repository.GetVotesByQuestionId(questionId));
            return Ok(votes);
        }

        // POST: api/votes/clear/1
        [HttpPost("clear/{questionId:int}")]
        public async Task<ActionResult> Clear(int questionId)
        {
            await _repository.Clear(questionId);            
            return Ok();
        }

    }
}
