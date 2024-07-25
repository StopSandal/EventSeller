using EventSeller.Helpers.Constants;
using EventSeller.Services.Interfaces.Exporters;
using EventSeller.Services.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventSeller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesStatisticsController : ControllerBase
    {
        private readonly ITicketSalesStatisticService _ticketSalesStatisticService;
        private readonly IResultExportService _resultExportService;
        private readonly ILogger<SalesStatisticsController> _logger;

        private const string TicketsStatisticsForEventFileName = "SalesStatisticsForEvent";
        private const string TicketsStatisticsForEventsFileName = "SalesStatisticsForEvents";
        private const string TicketsStatisticsForEventSessionFileName = "SalesStatisticsForEventSession";
        private const string TicketsStatisticsForEventAndSessionFileName = "SalesStatisticsForEventAndSession";
        private const string TicketsStatisticsForEventSessionsFileName = "SalesStatisticsForEventSessions";
        private const string TicketsStatisticsForPeriodFileName = "SalesStatisticsForTimePeriod";
        private const string TicketsStatisticsForEventTypeFileName = "SalesStatisticsForEventType";

        public SalesStatisticsController(ITicketSalesStatisticService ticketSalesStatisticService, IResultExportService resultExportService, ILogger<SalesStatisticsController> logger)
        {
            _ticketSalesStatisticService = ticketSalesStatisticService;
            _resultExportService = resultExportService;
            _logger = logger;
        }

        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("tickets/statistics/event/{eventId}/export")]
        public async Task<IActionResult> GetSalesStatisticForEventExportAsync(
            [FromRoute] long eventId,
            [FromQuery] string format = "json")
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForEventAsync(eventId);

                return await _resultExportService.ExportDataAsync(statistics, format, TicketsStatisticsForEventFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving sales statistics for event {eventId}: {ex.Message}");
                return BadRequest($"Error retrieving sales statistics for event {eventId}: {ex.Message}");
            }
        }

        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("tickets/statistics/events/export")]
        public async Task<IActionResult> GetSalesStatisticForEventsExportAsync(
            [FromQuery] IEnumerable<long> eventIds,
            [FromQuery] string format = "json")
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForEventsAsync(eventIds);

                return await _resultExportService.ExportDataAsync(statistics, format, TicketsStatisticsForEventsFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving sales statistics for events: {ex.Message}");
                return BadRequest($"Error retrieving sales statistics for events: {ex.Message}");
            }
        }


        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("tickets/statistics/event-session/{eventSessionId}/export")]
        public async Task<IActionResult> GetSalesStatisticForEventSessionExportAsync(
    long eventSessionId,
    [FromQuery] string format = "json")
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForEventSessionAsync(eventSessionId);

                return await _resultExportService.ExportDataAsync(statistics, format, TicketsStatisticsForEventSessionFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving sales statistics for event session {eventSessionId}: {ex.Message}");
                return BadRequest($"Error retrieving sales statistics for event session {eventSessionId}: {ex.Message}");
            }
        }

        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("tickets/statistics/event/{eventId}/session/{eventSessionId}/export")]
        public async Task<IActionResult> GetSalesStatisticForEventAndSessionExportAsync(
            long eventId,
            long eventSessionId,
            [FromQuery] string format = "json")
        {
            try
            {
                var result = await _ticketSalesStatisticService.GetSalesStatisticForEventAndSessionAsync(eventId, eventSessionId);

                return await _resultExportService.ExportDataAsync(result, format, TicketsStatisticsForEventAndSessionFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving sales statistics for event {eventId} and session {eventSessionId}: {ex.Message}");
                return BadRequest($"Error retrieving sales statistics for event {eventId} and session {eventSessionId}: {ex.Message}");
            }
        }

        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("tickets/statistics/event-sessions/export")]
        public async Task<IActionResult> GetSalesStatisticForEventSessionsExportAsync(
            [FromQuery] IEnumerable<long> eventSessionIds,
            [FromQuery] string format = "json")
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForEventSessionsAsync(eventSessionIds);

                return await _resultExportService.ExportDataAsync(statistics, format, TicketsStatisticsForEventSessionsFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving sales statistics for event sessions: {ex.Message}");
                return BadRequest($"Error retrieving sales statistics for event sessions: {ex.Message}");
            }
        }


        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("tickets/statistics/event-type/{eventTypeId}/export")]
        public async Task<IActionResult> GetSalesStatisticForEventTypeExportAsync(
            long eventTypeId,
            [FromQuery] string format = "json")
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForEventTypeAsync(eventTypeId);

                return await _resultExportService.ExportDataAsync(statistics, format, TicketsStatisticsForEventTypeFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving sales statistics for event type {eventTypeId}: {ex.Message}");
                return BadRequest($"Error retrieving sales statistics for event type {eventTypeId}: {ex.Message}");
            }
        }

        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("tickets/statistics/period/export")]
        public async Task<IActionResult> GetSalesStatisticForPeriodExportAsync(
            [FromQuery] DateTime firstPeriod,
            [FromQuery] DateTime secondPeriod,
            [FromQuery] string format = "json")
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForPeriodAsync(firstPeriod, secondPeriod);

                return await _resultExportService.ExportDataAsync(statistics, format, TicketsStatisticsForPeriodFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving sales statistics for period {firstPeriod} - {secondPeriod}: {ex.Message}");
                return BadRequest($"Error retrieving sales statistics for period {firstPeriod} - {secondPeriod}: {ex.Message}");
            }
        }

        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("tickets/statistics/week/export")]
        public async Task<IActionResult> GetSalesStatisticForWeekExportAsync(
            [FromQuery] DateTime weekStartDate,
            [FromQuery] string format = "json")
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForWeekAsync(weekStartDate);

                return await _resultExportService.ExportDataAsync(statistics, format, TicketsStatisticsForPeriodFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving sales statistics for week starting on {weekStartDate}: {ex.Message}");
                return BadRequest($"Error retrieving sales statistics for week starting on {weekStartDate}: {ex.Message}");
            }
        }

        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("tickets/statistics/day/export")]
        public async Task<IActionResult> GetSalesStatisticForDayExportAsync(
            [FromQuery] DateTime dayDate,
            [FromQuery] string format = "json")
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForDayAsync(dayDate);

                return await _resultExportService.ExportDataAsync(statistics, format, TicketsStatisticsForPeriodFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving sales statistics for day {dayDate}: {ex.Message}");
                return BadRequest($"Error retrieving sales statistics for day {dayDate}: {ex.Message}");
            }
        }
    }
}
