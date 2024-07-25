using EventSeller.Helpers.Constants;
using EventSeller.Services.Interfaces.Exporters;
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
        private readonly IResultExportService _resultExportService;
        private readonly ILogger<SectorsStatisticsController> _logger;

        private const string ForEventFileName = "SectorsPopularityForEvent";
        private const string HallFileName = "SectorsPopularityByHall";
        private const string HallGroupedByEventFileName = "SectorsPopularityByHallGrouped";


        public SectorsStatisticsController(ISectorsStatisticsService sectorsStatisticsService, IResultExportService resultExportService, ILogger<SectorsStatisticsController> logger)
        {
            _sectorsPopularityService = sectorsStatisticsService;
            _resultExportService = resultExportService;
            _logger = logger;
        }

        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("sectors/popularity/event/{eventId}/export")]
        public async Task<IActionResult> GetSectorsPopularityForEventExportAsync(
            long eventId,
            [FromQuery] int maxCount = 0,
            [FromQuery] string format = "json")
        {
            _logger.LogInformation("Retrieving sectors popularity for event {EventId} with max count {MaxCount} and format {Format}.",
                eventId,
                maxCount,
                format);

            try
            {
                var result = await _sectorsPopularityService.GetSectorsPopularityForEventAsync(eventId, maxCount);

                return await _resultExportService.ExportDataAsync(result, format, ForEventFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sectors popularity for event {EventId}.", eventId);
                return BadRequest($"An error occurred while retrieving sectors popularity for event {eventId}: {ex.Message}");
            }
        }

        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("sectors/popularity/hall/{placeHallId}/export")]
        public async Task<IActionResult> GetSectorsPopularityInHallExportAsync(
            long placeHallId,
            [FromQuery] int maxCount = 0,
            [FromQuery] string format = "json")
        {
            _logger.LogInformation("Retrieving sectors popularity for hall {PlaceHallId} with max count {MaxCount} and format {Format}.",
                placeHallId,
                maxCount,
                format);

            try
            {
                var result = await _sectorsPopularityService.GetSectorsPopularityInHallAsync(placeHallId, maxCount);

                return await _resultExportService.ExportDataAsync(result, format, HallFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sectors popularity for hall {PlaceHallId}.", placeHallId);
                return BadRequest($"An error occurred while retrieving sectors popularity for hall {placeHallId}: {ex.Message}");
            }
        }

        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("sectors/popularity/by-groups/hall/{placeHallId}/export")]
        public async Task<IActionResult> GetSectorsPopularityByEventGroupsAtHallExportAsync(
            long placeHallId,
            [FromQuery] IEnumerable<long> eventIds,
            [FromQuery] int maxCount = 0,
            [FromQuery] string format = "json")
        {
            _logger.LogInformation("Retrieving sectors popularity by event groups for hall {PlaceHallId} with event IDs {EventIds} and max count {MaxCount}.",
                placeHallId,
                string.Join(",", eventIds),
                maxCount);

            try
            {
                var result = await _sectorsPopularityService.GetSectorsPopularityByEventGroupsAtHallAsync(placeHallId, eventIds, maxCount);

                return await _resultExportService.ExportDataAsync(result, format, HallGroupedByEventFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sectors popularity by event groups for hall {PlaceHallId}.", placeHallId);
                return BadRequest($"An error occurred while retrieving sectors popularity by event groups for hall {placeHallId}: {ex.Message}");
            }
        }
    }
}
