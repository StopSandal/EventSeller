using EventSeller.Helpers.Constants;
using EventSeller.Services.Interfaces.Exporters;
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
        private readonly IResultExportService _resultExportService;
        private readonly ILogger<SeatsStatisticsController> _logger;

        private const string ForEventFileName = "SeatsPopularityForEvent";
        private const string HallFileName = "SeatsPopularityByHall";
        private const string HallGroupedByEventFileName = "SeatsPopularityByHallGrouped";


        public SeatsStatisticsController(ISeatsPopularityService seatsPopularityService, IResultExportService resultExportService, ILogger<SeatsStatisticsController> logger)
        {
            _seatsPopularityService = seatsPopularityService;
            _resultExportService = resultExportService;
            _logger = logger;
        }

        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("seats/popularity/event/{eventId}/export")]
        public async Task<IActionResult> GetSeatsPopularityForEventExportAsync(
            long eventId,
            [FromQuery] int maxCount = 0,
            [FromQuery] string format = "json")
        {
            _logger.LogInformation("Retrieving seats popularity for event {EventId} with max count {MaxCount} and format {Format}.", eventId, maxCount, format);

            try
            {
                var result = await _seatsPopularityService.GetSeatsPopularityForEventAsync(eventId, maxCount);

                return await _resultExportService.ExportDataAsync(result, format, ForEventFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving seats popularity for event {EventId}.", eventId);
                return BadRequest($"An error occurred while retrieving seats popularity for event {eventId}: {ex.Message}");
            }
        }


        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("seats/popularity/hall/{placeHallId}/export")]
        public async Task<IActionResult> GetSeatsPopularityInHallExportAsync(
            long placeHallId,
            [FromQuery] int maxCount = 0,
            [FromQuery] string format = "json")
        {
            _logger.LogInformation("Retrieving seats popularity in hall {PlaceHallId} with max count {MaxCount} and format {Format}.", placeHallId, maxCount, format);

            try
            {
                var result = await _seatsPopularityService.GetSeatsPopularityInHallAsync(placeHallId, maxCount);

                return await _resultExportService.ExportDataAsync(result, format, HallFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving seats popularity in hall {PlaceHallId}.", placeHallId);
                return BadRequest($"An error occurred while retrieving seats popularity in hall {placeHallId}: {ex.Message}");
            }
        }

        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("seats/popularity/by-groups/hall/{placeHallId}/export")]
        public async Task<IActionResult> GetSeatsPopularityByEventGroupsAtHallExportAsync(
            long placeHallId,
            [FromQuery] IEnumerable<long> eventIds,
            [FromQuery] int maxCount = 0,
            [FromQuery] string format = "json")
        {
            _logger.LogInformation("Retrieving seats popularity by event groups at hall {PlaceHallId} with event IDs {EventIds}, max count {MaxCount}, and format {Format}.",
                placeHallId,
                string.Join(",", eventIds),
                maxCount,
                format);

            try
            {
                var result = await _seatsPopularityService.GetSeatsPopularityByEventGroupsAtHallAsync(placeHallId, eventIds, maxCount);

                return await _resultExportService.ExportDataAsync(result, format, HallGroupedByEventFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving seats popularity by event groups at hall {PlaceHallId}.", placeHallId);
                return BadRequest($"An error occurred while retrieving seats popularity by event groups at hall {placeHallId}: {ex.Message}");
            }
        }
    }
}
