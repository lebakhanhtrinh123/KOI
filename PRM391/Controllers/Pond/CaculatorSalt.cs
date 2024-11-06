using BusinessLayer.Request;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace KOI.Controllers.Pond
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaculatorSalt : Controller
    {
        private readonly ISaltCalculationService _saltCalculationService;

        public CaculatorSalt(ISaltCalculationService saltCalculationService)
        {
            _saltCalculationService = saltCalculationService;
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> CalculateSaltAmount([FromBody] CaculatorSaltRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }

            var result = await _saltCalculationService.CalculateSaltAmount(request);

            return Ok(result);
        }
    }

}
