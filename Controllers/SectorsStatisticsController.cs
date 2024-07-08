using EventSeller.Services.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventSeller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectorsStatisticsController : ControllerBase
    {
        private readonly ISectorsStatisticsService _sectorsPopularityService;

        public SectorsStatisticsController(ISectorsStatisticsService sectorsStatisticsService)
        {
            _sectorsPopularityService = sectorsStatisticsService;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("sectors/popularity/event/{eventId}")]
        public async Task<IActionResult> GetSectorsPopularityForEvent(long eventId, [FromQuery] int maxCount = 0)
        {
            try
            {
                var result = await _sectorsPopularityService.GetSectorsPopularityForEventAsync(eventId, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("sectors/popularity/hall/{placeHallId}")]
        public async Task<IActionResult> GetSectorsPopularityInHall(long placeHallId, [FromQuery] int maxCount = 0)
        {
            try
            {
                var result = await _sectorsPopularityService.GetSectorsPopularityInHallAsync(placeHallId, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("sectors/popularity/by-groups/hall/{placeHallId}")]
        public async Task<IActionResult> GetSectorsPopularityByEventGroupsAtHall(long placeHallId, [FromQuery] IEnumerable<long> eventIds, int maxCount = 0)
        {
            try
            {
                var result = await _sectorsPopularityService.GetSectorsPopularityByEventGroupsAtHallAsync(placeHallId, eventIds, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
