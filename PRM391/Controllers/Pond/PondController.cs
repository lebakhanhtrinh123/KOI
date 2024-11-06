using BusinessLayer.Response;
using Microsoft.AspNetCore.Mvc;
using RepoitoryLayer.Interface;

namespace KOI.Controllers.Pond
{
    [ApiController]
    [Route("api/[controller]")]
    public class PondsController : ControllerBase
    {
        private readonly IPondsRepository _pondsRepository;

        public PondsController(IPondsRepository pondsRepository)
        {
            _pondsRepository = pondsRepository;
        }

        // GET: api/Ponds
        [HttpGet("get-all-pond")]
        public async Task<ActionResult<List<PondsResponse>>> GetAllPonds()
        {
            var ponds = await _pondsRepository.GetAllPonds();

            if (ponds == null || ponds.Count == 0)
            {
                return NotFound("No ponds found.");
            }

            return Ok(ponds);
        }
        [HttpGet("get-pond/{id}")]
        public async Task<ActionResult<PondsResponse>> GetPondsById(int id)
        {
            var pond = await _pondsRepository.GetPondsById(id);

            if (pond == null)
            {
                return NotFound($"Pond with ID {id} not found.");
            }

            return Ok(pond);
        }

        [HttpGet("get-pond-by-userId/{userId}")]
        public async Task<ActionResult<PondsResponse>> GetPondsByUserId(int userId)
        {
            var pond = await _pondsRepository.GetAllPondsByUserId(userId);

            if (pond == null)
            {
                return NotFound($"Pond with ID {userId} not found.");
            }

            return Ok(pond);
        }
    }
}
