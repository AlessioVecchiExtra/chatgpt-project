using Microsoft.AspNetCore.Mvc;
using EventAPI.Models;
using EventAPI.Repositories;

namespace MyEventApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : ControllerBase
    {
        private readonly IVoteRepository _repository;

        public VotesController(IVoteRepository repository)
        {
            _repository = repository;
        }

        // POST: api/Votes
        [HttpPost]
        public async Task<IActionResult> PostVote([FromBody] Vote vote)
        {
            if (vote == null || string.IsNullOrWhiteSpace(vote.Word))
            {
                return BadRequest("Vote or word is invalid.");
            }

            await _repository.AddVoteAsync(vote);
            return Ok();
        }

        // GET: api/Votes
        [HttpGet]
        public async Task<ActionResult<List<Vote>>> GetAllVotes()
        {
            var votes = await _repository.GetAllVotesAsync();
            return Ok(votes);
        }

        // GET: api/Votes/session/1
        [HttpGet("session/{sessionId:int}")]
        public async Task<ActionResult<List<Vote>>> GetVotesBySession(int sessionId)
        {
            var votes = await _repository.GetVotesBySessionAsync(sessionId);
            return Ok(votes);
        }
    }
}
