using BusinessLayer.Response;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Implement;
using ServiceLayer.Interface;

namespace KOI.Controllers.FeedSchedule
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedScheduleController : Controller
    {
        private readonly IFeedScheduleService _feedScheduleService;

        public FeedScheduleController(IFeedScheduleService feedScheduleService)
        {
            _feedScheduleService = feedScheduleService;
        }

        [HttpGet("CalculateFeed/{koiGrowthId}")]
        public async Task<IActionResult> CalculateFeed(int koiGrowthId)
        {
            var feedSchedule = await _feedScheduleService.CaculatorFeed(koiGrowthId);
            if (feedSchedule == null)
            {
                return BadRequest("Không tìm thấy thông tin tăng trưởng cá.");
            }
            return Ok(feedSchedule);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateFeedSchedule(int koiGrowthId, [FromBody] FeedScheduleRequest feedScheduleRequest)
        {
            var result = await _feedScheduleService.CreateFeedSchedule(koiGrowthId, feedScheduleRequest);
            if (result == "create FeedSchedule successfully")
            {
                return Ok(new { message = result });
            }
            return BadRequest(new { message = result });
        }

        [HttpDelete("Delete/{feedScheduleId}")]
        public async Task<IActionResult> DeleteFeedSchedule(int feedScheduleId)
        {
            var success = await _feedScheduleService.DeleteFeedSchedule(feedScheduleId);
            if (success)
            {
                return Ok(new { message = "Feed schedule deleted successfully" });
            }
            return NotFound(new { message = "Feed schedule not found" });
        }

        [HttpGet("GetByKoiGrowthId/{koiGrowthId}")]
        public async Task<IActionResult> GetFeedScheduleByKoiGrowthID(int koiGrowthId)
        {
            var feedSchedules = await _feedScheduleService.GetFeedScheduleByKoiGrowthID(koiGrowthId);
            if (feedSchedules == null || !feedSchedules.Any())
            {
                return NotFound(new { message = "No feed schedules found for this Koi growth ID" });
            }
            return Ok(feedSchedules);
        }
    }
}
