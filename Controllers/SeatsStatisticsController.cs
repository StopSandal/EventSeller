using EventSeller.Services.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventSeller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatsStatisticsController : ControllerBase
    {
        private readonly ISeatsPopularityService _seatsPopularityService;

        public SeatsStatisticsController(ISeatsPopularityService seatsPopularityService)
        {
            _seatsPopularityService = seatsPopularityService;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("seats/popularity/event/{eventId}")]
        public async Task<IActionResult> GetSeatsPopularityForEvent(long eventId, [FromQuery] int maxCount = 0)
        {
            try
            {
                var result = await _seatsPopularityService.GetSeatsPopularityForEventAsync(eventId, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("seats/popularity/hall/{placeHallId}")]
        public async Task<IActionResult> GetSeatsPopularityInHall(long placeHallId, [FromQuery] int maxCount = 0)
        {
            try
            {
                var result = await _seatsPopularityService.GetSeatsPopularityInHallAsync(placeHallId, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("seats/popularity/by-groups/hall/{placeHallId}")]
        public async Task<IActionResult> GetSeatsPopularityByEventGroupsAtHall(long placeHallId, [FromQuery] IEnumerable<long> eventIds, int maxCount = 0)
        {
            try
            {
                var result = await _seatsPopularityService.GetSeatsPopularityByEventGroupsAtHallAsync(placeHallId, eventIds, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
