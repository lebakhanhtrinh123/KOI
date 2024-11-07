using BusinessLayer.Request;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace KOI.Controllers.Koi
{
    [ApiController]
    [Route("[controller]")]
    public class KoiFishController : Controller
    {
        private readonly IKoiService _fishService;

        public KoiFishController(IKoiService fishService)
        {
            _fishService = fishService;
        }
        [HttpGet("get-koi-by-pondId/{pondId}")]
        public async Task<ActionResult> GetKoiByPondId(int pondId)
        {
            var getKoi = await _fishService.GetKoiFishByPondId(pondId);
            return Ok(getKoi);
        }

        [HttpGet("get-koi-by-Id/{id}")]
        public async Task<ActionResult> GetKoiById(int id)
        {
            var getKoi = await _fishService.GetKoiFishById(id);
            return Ok(getKoi);
        }
        [HttpPost("create-koi/{pondId}")]
        public async Task<ActionResult> CreateKoiFish(int pondId, [FromBody] KoiFishRequest koiFishRequest)
        {
            if (koiFishRequest == null)
            {
                return BadRequest("Invalid data.");
            }

            // Call service method to create a Koi fish
            var result = await _fishService.CreateKoiFish(pondId, koiFishRequest);

            if (result == "Create Koi successfully")
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpDelete("delete-koi/{id}")]
        public async Task<ActionResult> DeleteKoiFish(int id)
        {
            bool deleteResult = await _fishService.DeleteKoiFish(id);
            if (deleteResult)
            {
                return Ok(new { message = "Koi fish deleted successfully." }); 
            }
            else
            {
                return NotFound(new { message = "Koi fish not found." });
            }
        }
        [HttpPut("update-koi/{id}")]
        public async Task<ActionResult> UpdateKoiFish(int id,KoiFishRequest koiFishRequest)
        {
            var updateKoifish = await _fishService.UpdateKoiFish(id, koiFishRequest);
            return Ok(updateKoifish);

        }

    }
}
