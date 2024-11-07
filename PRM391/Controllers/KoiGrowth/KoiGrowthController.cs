using BusinessLayer.Request;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace KOI.Controllers.KoiGrowth
{
    [Route("api/[controller]")]
    [ApiController]
    public class KoiGrowthController : Controller
    {
        private readonly IKoiGrowthService _koiGrowthService;

        public KoiGrowthController(IKoiGrowthService koiGrowthService)
        {
            _koiGrowthService = koiGrowthService;
        }

        [HttpPost("create-koigrowth/{koiId}")]
        public async Task<IActionResult> CreateKoiGrowth(int koiId, [FromBody] KoiGrowthRequest koiGrowthRequest)
        {
            if (koiGrowthRequest == null)
            {
                return BadRequest(new { message = "Invalid request data." });
            }

            string result = await _koiGrowthService.CreateKoiGrowth(koiId, koiGrowthRequest);
            if (result == "Create KoiGrowth successfully")
            {
                return Ok(new { message = result });
            }
            return BadRequest(new { message = result });
        }
        [HttpDelete("delete-koigrowth/{koiGrowthId}")]
        public async Task<IActionResult> DeleteKoiGrowth(int koiGrowthId)
        {
            bool delete = await _koiGrowthService.DeleteKoiGrowth(koiGrowthId);
            if (delete)
            {
                return Ok(new { message = "KoiGrowth deleted successfully" });
            }
            return NotFound(new { message = "KoiGrowth not found" });
        }
        [HttpGet("get-koigrowth-by-id/{koiId}")]
        public async Task<IActionResult> GetKoiGrowths(int koiId)
        {
            var koiGrowths = await _koiGrowthService.GetKoiGrowths(koiId);
            if (koiGrowths == null || koiGrowths.Count == 0)
            {
                return NotFound(new { message = "No growth records found for the specified koi." });
            }

            return Ok(koiGrowths);
        }
    }
}
