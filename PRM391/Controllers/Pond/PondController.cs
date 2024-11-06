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
        [HttpGet]
        public async Task<ActionResult<List<PondsResponse>>> GetAllPonds()
        {
            var ponds = await _pondsRepository.GetAllPonds();

            if (ponds == null || ponds.Count == 0)
            {
                return NotFound("No ponds found.");
            }

            return Ok(ponds);
        }
    }
}
