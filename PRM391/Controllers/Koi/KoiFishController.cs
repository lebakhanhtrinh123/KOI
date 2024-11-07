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
    }
}
