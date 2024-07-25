using EventSeller.Helpers.Constants;
using EventSeller.Services.Interfaces.Exporters;
using EventSeller.Services.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventSeller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventPopularityController : ControllerBase
    {
        private readonly IEventPopularityService _eventPopularityService;
        private readonly IResultExportService _resultExportService;
        private readonly ILogger<EventPopularityController> _logger;

        private const string EventsPopularityByPeriodFileName = "EventPopularityByPeriod";
        private const string EventTypesPopularityFileName = "EventTypesPopularity";
        private const string MostPopularEventsFileName = "MostPopularEvents";
        private const string MostPopularEventTypesFileName = "MostPopularEventTypes";
        private const string MostRealizableEventTypesFileName = "MostRealizableEventTypes";
        private const string MostRealizableEventsFileName = "MostRealizableEvents";


        public EventPopularityController(IEventPopularityService eventPopularityService, ILogger<EventPopularityController> logger, IResultExportService resultExportService)
        {
            _eventPopularityService = eventPopularityService;
            _resultExportService = resultExportService;
            _logger = logger;
        }

        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("events/popularity/export")]
        public async Task<IActionResult> GetEventsPopularityByPeriodExportAsync(
            [FromQuery] DateTime startDateTime,
            [FromQuery] DateTime endDateTime,
            [FromQuery] string format = "json")
        {
            try
            {
                var result = await _eventPopularityService.GetEventsPopularityByPeriodAsync(startDateTime, endDateTime);

                return await _resultExportService.ExportDataAsync(result, format, EventsPopularityByPeriodFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving events popularity.");
                return BadRequest($"Error retrieving events popularity: {ex.Message}");
            }
        }

        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("eventtypes/statistic/{eventTypeId}/export")]
        public async Task<IActionResult> GetEventTypeStatisticExportAsync(
            [FromRoute] long eventTypeId,
            [FromQuery] string format = "json")
        {
            try
            {
                var result = await _eventPopularityService.GetEventTypeStatisticAsync(eventTypeId);
                return await _resultExportService.ExportDataAsync(result, format, EventTypesPopularityFileName);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving event type statistic: {ex.Message}");
                return BadRequest($"Error retrieving event type statistic: {ex.Message}");
            }
        }

        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("events/popular/{topCount}/export")]
        public async Task<IActionResult> GetMostPopularEventsExportAsync(
            [FromRoute] int topCount,
            [FromQuery] string format = "json")
        {
            try
            {
                var result = await _eventPopularityService.GetMostPopularEventsAsync(topCount);

                return await _resultExportService.ExportDataAsync(result, format, MostPopularEventsFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving most popular events: {ex.Message}");
                return BadRequest($"Error retrieving most popular events: {ex.Message}");
            }
        }

        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("eventtypes/popular/{topCount}/export")]
        public async Task<IActionResult> GetMostPopularEventTypesExportAsync(
            [FromRoute] int topCount,
            [FromQuery] string format = "json")
        {
            try
            {
                var result = await _eventPopularityService.GetMostPopularEventTypesAsync(topCount);

                return await _resultExportService.ExportDataAsync(result, format, MostPopularEventTypesFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving most popular event types: {ex.Message}");
                return BadRequest($"Error retrieving most popular event types: {ex.Message}");
            }
        }

        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("events/realizable/{topCount}/export")]
        public async Task<IActionResult> GetMostRealizableEventsExportAsync(
            [FromRoute] int topCount,
            [FromQuery] string format = "json")
        {
            try
            {
                var result = await _eventPopularityService.GetMostRealizableEventsAsync(topCount);

                return await _resultExportService.ExportDataAsync(result, format, MostRealizableEventsFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving most realizable events: {ex.Message}");
                return BadRequest($"Error retrieving most realizable events: {ex.Message}");
            }
        }

        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("eventtypes/realizable/{topCount}/export")]
        public async Task<IActionResult> GetMostRealizableEventTypesExportAsync(
            [FromRoute] int topCount,
            [FromQuery] string format = "json")
        {
            try
            {
                var result = await _eventPopularityService.GetMostRealizableEventTypesAsync(topCount);

                return await _resultExportService.ExportDataAsync(result, format, MostRealizableEventTypesFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving most realizable event types: {ex.Message}");
                return BadRequest($"Error retrieving most realizable event types: {ex.Message}");
            }
        }
    }
}
