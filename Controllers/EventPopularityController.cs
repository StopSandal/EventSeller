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
        private readonly IExcelFileExport _excelFileExport;
        private readonly ICsvFileExport _csvFileExport;
        private readonly ILogger<EventPopularityController> _logger;

        private const string EventsPopularityByPeriodFileName = "EventPopularityByPeriod";
        private const string EventTypesPopularityFileName = "EventTypesPopularity";
        private const string MostPopularEventsFileName = "MostPopularEvents";
        private const string MostPopularEventTypesFileName = "MostPopularEventTypes";
        private const string MostRealizableEventTypesFileName = "MostRealizableEventTypes";
        private const string MostRealizableEventsFileName = "MostRealizableEvents";


        public EventPopularityController(IEventPopularityService eventPopularityService, ILogger<EventPopularityController> logger, IExcelFileExport excelFileExport, ICsvFileExport csvFileExport)
        {
            _eventPopularityService = eventPopularityService;
            _logger = logger;
            _excelFileExport = excelFileExport;
            _csvFileExport = csvFileExport;
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

                switch (format.ToLower())
                {
                    case "excel":
                        var excelStream = await _excelFileExport.ExportFileAsync(result);
                        return File(excelStream, HelperConstants.ExcelContentType, $"{EventsPopularityByPeriodFileName}{HelperConstants.ExcelExtension}");

                    case "csv":
                        var csvStream = await _csvFileExport.ExportFileAsync(result);
                        return File(csvStream, HelperConstants.CsvContentType, $"{EventsPopularityByPeriodFileName}{HelperConstants.CsvExtension}");

                    case "json":
                        return Ok(result);

                    default:
                        return BadRequest("Invalid format specified. Supported formats are 'json', 'excel', and 'csv'.");
                }
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
                if (result == null)
                {
                    return NotFound();
                }

                switch (format.ToLower())
                {
                    case "excel":
                        var excelStream = await _excelFileExport.ExportFileAsync(result);
                        return File(excelStream, HelperConstants.ExcelContentType, $"{EventTypesPopularityFileName}{HelperConstants.ExcelExtension}");

                    case "csv":
                        var csvStream = await _csvFileExport.ExportFileAsync(result);
                        return File(csvStream, HelperConstants.CsvContentType, $"{EventTypesPopularityFileName}{HelperConstants.CsvExtension}");

                    case "json":
                        return Ok(result);

                    default:
                        return BadRequest("Invalid format specified. Supported formats are 'json', 'excel', and 'csv'.");
                }
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

                switch (format.ToLower())
                {
                    case "excel":
                        var excelStream = await _excelFileExport.ExportFileAsync(result);
                        return File(excelStream, HelperConstants.ExcelContentType, $"{MostPopularEventsFileName}{HelperConstants.ExcelExtension}");

                    case "csv":
                        var csvStream = await _csvFileExport.ExportFileAsync(result);
                        return File(csvStream, HelperConstants.CsvContentType, $"{MostPopularEventsFileName}{HelperConstants.CsvExtension}");

                    case "json":
                        return Ok(result);

                    default:
                        return BadRequest("Invalid format specified. Supported formats are 'json', 'excel', and 'csv'.");
                }
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

                switch (format.ToLower())
                {
                    case "excel":
                        var excelStream = await _excelFileExport.ExportFileAsync(result);
                        return File(excelStream, HelperConstants.ExcelContentType, $"{MostPopularEventTypesFileName}{HelperConstants.ExcelExtension}");

                    case "csv":
                        var csvStream = await _csvFileExport.ExportFileAsync(result);
                        return File(csvStream, HelperConstants.CsvContentType, $"{MostPopularEventTypesFileName}{HelperConstants.CsvExtension}");

                    case "json":
                        return Ok(result);

                    default:
                        return BadRequest("Invalid format specified. Supported formats are 'json', 'excel', and 'csv'.");
                }
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

                switch (format.ToLower())
                {
                    case "excel":
                        var excelStream = await _excelFileExport.ExportFileAsync(result);
                        return File(excelStream, HelperConstants.ExcelContentType, $"{MostRealizableEventsFileName}{HelperConstants.ExcelExtension}");

                    case "csv":
                        var csvStream = await _csvFileExport.ExportFileAsync(result);
                        return File(csvStream, HelperConstants.CsvContentType, $"{MostRealizableEventsFileName}{HelperConstants.CsvExtension}");

                    case "json":
                        return Ok(result);

                    default:
                        return BadRequest("Invalid format specified. Supported formats are 'json', 'excel', and 'csv'.");
                }
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

                switch (format.ToLower())
                {
                    case "excel":
                        var excelStream = await _excelFileExport.ExportFileAsync(result);
                        return File(excelStream, HelperConstants.ExcelContentType, $"{MostRealizableEventTypesFileName}{HelperConstants.ExcelExtension}");

                    case "csv":
                        var csvStream = await _csvFileExport.ExportFileAsync(result);
                        return File(csvStream, HelperConstants.CsvContentType, $"{MostRealizableEventTypesFileName}{HelperConstants.CsvExtension}");

                    case "json":
                        return Ok(result);

                    default:
                        return BadRequest("Invalid format specified. Supported formats are 'json', 'excel', and 'csv'.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving most realizable event types: {ex.Message}");
                return BadRequest($"Error retrieving most realizable event types: {ex.Message}");
            }
        }
    }
}
