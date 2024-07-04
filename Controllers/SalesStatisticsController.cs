using EventSeller.DataLayer.EntitiesDto.Statistics;
using EventSeller.Services.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventSeller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesStatisticsController : ControllerBase
    {
        private readonly ITicketSalesStatisticService _ticketSalesStatisticService;

        public SalesStatisticsController(ITicketSalesStatisticService ticketSalesStatisticService)
        {
            _ticketSalesStatisticService = ticketSalesStatisticService;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("event/{eventId}")]
        public async Task<ActionResult<SalesStatisticsDTO>> GetSalesStatisticForEventAsync(long eventId)
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForEventAsync(eventId);
                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving sales statistics for event {eventId}: {ex.Message}");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("events")]
        public async Task<ActionResult<SalesStatisticsDTO>> GetSalesStatisticForEventsAsync([FromQuery] IEnumerable<long> eventIds)
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForEventsAsync(eventIds);
                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving sales statistics for events: {ex.Message}");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("event-session/{eventSessionId}")]
        public async Task<ActionResult<SalesStatisticsDTO>> GetSalesStatisticForEventSessionAsync(long eventSessionId)
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForEventSessionAsync(eventSessionId);
                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving sales statistics for event session {eventSessionId}: {ex.Message}");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("event/{eventId}/session/{eventSessionId}")]
        public async Task<IActionResult> GetSalesStatisticForEventAndSession(long eventId, long eventSessionId)
        {
            try
            {
                var result = await _ticketSalesStatisticService.GetSalesStatisticForEventAndSessionAsync(eventId, eventSessionId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("event-sessions")]
        public async Task<ActionResult<SalesStatisticsDTO>> GetSalesStatisticForEventSessionsAsync([FromQuery] IEnumerable<long> eventSessionIds)
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForEventSessionsAsync(eventSessionIds);
                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving sales statistics for event sessions: {ex.Message}");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("event-type/{eventTypeId}")]
        public async Task<ActionResult<SalesStatisticsDTO>> GetSalesStatisticForEventTypeAsync(long eventTypeId)
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForEventTypeAsync(eventTypeId);
                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving sales statistics for event type {eventTypeId}: {ex.Message}");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("period")]
        public async Task<ActionResult<SalesStatisticsDTO>> GetSalesStatisticForPeriodAsync([FromQuery] DateTime firstPeriod, [FromQuery] DateTime secondPeriod)
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForPeriodAsync(firstPeriod, secondPeriod);
                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving sales statistics for period {firstPeriod} - {secondPeriod}: {ex.Message}");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("week")]
        public async Task<ActionResult<SalesStatisticsDTO>> GetSalesStatisticForWeekAsync([FromQuery] DateTime weekStartDate)
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForWeekAsync(weekStartDate);
                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving sales statistics for week starting on {weekStartDate}: {ex.Message}");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("day")]
        public async Task<ActionResult<SalesStatisticsDTO>> GetSalesStatisticForDayAsync([FromQuery] DateTime dayDate)
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForDayAsync(dayDate);
                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving sales statistics for day {dayDate}: {ex.Message}");
            }
        }
    }
}
