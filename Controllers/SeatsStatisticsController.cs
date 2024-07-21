using EventSeller.Helpers;
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
        private readonly IExcelFileExport _excelFileExport;
        private readonly ICsvFileExport _csvFileExport;
        private readonly ILogger<SeatsStatisticsController> _logger;

        private const string ForEventFileName = "SeatsPopularityForEvent";
        private const string HallFileName = "SeatsPopularityByHall";
        private const string HallGroupedByEventFileName = "SeatsPopularityByHallGrouped";


        public SeatsStatisticsController(ISeatsPopularityService seatsPopularityService, IExcelFileExport excelFileExport, ICsvFileExport csvFileExport, ILogger<SeatsStatisticsController> logger)
        {
            _seatsPopularityService = seatsPopularityService;
            _excelFileExport = excelFileExport;
            _csvFileExport = csvFileExport;
            _logger = logger;
        }

        [Authorize(Policy = "AdminOnly")]
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

                switch (format.ToLower())
                {
                    case "excel":
                        var excelStream = await _excelFileExport.ExportFileAsync(result);
                        return File(excelStream, HelperConstants.ExcelContentType, $"{ForEventFileName}{HelperConstants.ExcelExtension}");

                    case "csv":
                        var csvStream = await _csvFileExport.ExportFileAsync(result);
                        return File(csvStream, HelperConstants.CsvContentType, $"{ForEventFileName}{HelperConstants.CsvExtension}");

                    case "json":
                        return Ok(result);

                    default:
                        return BadRequest("Invalid format specified. Supported formats are 'json', 'excel', and 'csv'.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving seats popularity for event {EventId}.", eventId);
                return BadRequest($"An error occurred while retrieving seats popularity for event {eventId}: {ex.Message}");
            }
        }


        [Authorize(Policy = "AdminOnly")]
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

                switch (format.ToLower())
                {
                    case "excel":
                        var excelStream = await _excelFileExport.ExportFileAsync(result);
                        return File(excelStream, HelperConstants.ExcelContentType, $"{HallFileName}{HelperConstants.ExcelExtension}");

                    case "csv":
                        var csvStream = await _csvFileExport.ExportFileAsync(result);
                        return File(csvStream, HelperConstants.CsvContentType, $"{HallFileName}{HelperConstants.CsvExtension}");

                    case "json":
                        return Ok(result);

                    default:
                        return BadRequest("Invalid format specified. Supported formats are 'json', 'excel', and 'csv'.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving seats popularity in hall {PlaceHallId}.", placeHallId);
                return BadRequest($"An error occurred while retrieving seats popularity in hall {placeHallId}: {ex.Message}");
            }
        }

        [Authorize(Policy = "AdminOnly")]
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

                switch (format.ToLower())
                {
                    case "excel":
                        var excelStream = await _excelFileExport.ExportFileAsync(result);
                        return File(excelStream, HelperConstants.ExcelContentType, $"{HallGroupedByEventFileName}{HelperConstants.ExcelExtension}");

                    case "csv":
                        var csvStream = await _csvFileExport.ExportFileAsync(result);
                        return File(csvStream, HelperConstants.CsvContentType, $"{HallGroupedByEventFileName}{HelperConstants.CsvExtension}");

                    case "json":
                        return Ok(result);

                    default:
                        return BadRequest("Invalid format specified. Supported formats are 'json', 'excel', and 'csv'.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving seats popularity by event groups at hall {PlaceHallId}.", placeHallId);
                return BadRequest($"An error occurred while retrieving seats popularity by event groups at hall {placeHallId}: {ex.Message}");
            }
        }
    }
}
