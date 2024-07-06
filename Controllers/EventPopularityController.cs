using EventSeller.DataLayer.EntitiesDto.Statistics;
using EventSeller.Services.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventSeller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventPopularityController : ControllerBase
    {
        private readonly IEventPopularityService _eventPopularityService;
        private readonly ILogger<EventPopularityController> _logger;

        public EventPopularityController(IEventPopularityService eventPopularityService, ILogger<EventPopularityController> logger)
        {
            _eventPopularityService = eventPopularityService;
            _logger = logger;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("events/popularity")]
        public async Task<ActionResult<IEnumerable<EventPopularityStatistic>>> GetEventsPopularityByPeriod(DateTime startDateTime, DateTime endDateTime)
        {
            try
            {
                var result = await _eventPopularityService.GetEventsPopularityByPeriod(startDateTime, endDateTime);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving events popularity: {ex.Message}");
                return BadRequest("Error retrieving events popularity");
            }
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("eventtypes/{eventTypeId}/statistic")]
        public async Task<ActionResult<EventTypePopularityStatisticDTO>> GetEventTypeStatistic(long eventTypeId)
        {
            try
            {
                var result = await _eventPopularityService.GetEventTypeStatistic(eventTypeId);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving event type statistic: {ex.Message}");
                return BadRequest("Error retrieving event type statistic");
            }
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("events/popular/{topCount}")]
        public async Task<ActionResult<IEnumerable<EventPopularityStatistic>>> GetMostPopularEvents(int topCount)
        {
            try
            {
                var result = await _eventPopularityService.GetMostPopularEvents(topCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving most popular events: {ex.Message}");
                return BadRequest("Error retrieving most popular events");
            }
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("eventtypes/popular/{topCount}")]
        public async Task<ActionResult<IEnumerable<EventTypePopularityStatisticDTO>>> GetMostPopularEventTypes(int topCount)
        {
            try
            {
                var result = await _eventPopularityService.GetMostPopularEventTypes(topCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving most popular event types: {ex.Message}");
                return BadRequest("Error retrieving most popular event types");
            }
        }

        [HttpGet("events/realizable/{topCount}")]
        public async Task<ActionResult<IEnumerable<EventPopularityStatistic>>> GetMostRealizableEvents(int topCount)
        {
            try
            {
                var result = await _eventPopularityService.GetMostRealizableEvents(topCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving most realizable events: {ex.Message}");
                return BadRequest("Error retrieving most realizable events");
            }
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("eventtypes/realizable/{topCount}")]
        public async Task<ActionResult<IEnumerable<EventTypePopularityStatisticDTO>>> GetMostRealizableEventTypes(int topCount)
        {
            try
            {
                var result = await _eventPopularityService.GetMostRealizableEventTypes(topCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving most realizable event types: {ex.Message}");
                return BadRequest("Error retrieving most realizable event types");
            }
        }
    }
}
