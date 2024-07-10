using EventSeller.Helpers;
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
        private const string EventTypePopularityFileName = "EventTypePopularity";
        private const string MostPopularEventsFileName = "MostPopularEvents";
        private const string MostRealizableEventTypesFileName = "MostRealizableEventTypes";
        private const string MostRealizableEventsFileName = "MostRealizableEvents";


        public EventPopularityController(IEventPopularityService eventPopularityService, ILogger<EventPopularityController> logger, IExcelFileExport excelFileExport, ICsvFileExport csvFileExport)
        {
            _eventPopularityService = eventPopularityService;
            _logger = logger;
            _excelFileExport = excelFileExport;
            _csvFileExport = csvFileExport;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("events/popularity")]
        public async Task<IActionResult> GetEventsPopularityByPeriodAsync(DateTime startDateTime, DateTime endDateTime)
        {
            try
            {
                var result = await _eventPopularityService.GetEventsPopularityByPeriod(startDateTime, endDateTime);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving events popularity: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("eventtypes/statistic/{eventTypeId}")]
        public async Task<IActionResult> GetEventTypeStatisticAsync(long eventTypeId)
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
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("events/popular/{topCount}")]
        public async Task<IActionResult> GetMostPopularEventsAsync(int topCount)
        {
            try
            {
                var result = await _eventPopularityService.GetMostPopularEvents(topCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving most popular events: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("eventtypes/popular/{topCount}")]
        public async Task<IActionResult> GetMostPopularEventTypesAsync(int topCount)
        {
            try
            {
                var result = await _eventPopularityService.GetMostPopularEventTypes(topCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving most popular event types: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("events/realizable/{topCount}")]
        public async Task<IActionResult> GetMostRealizableEventsAsync(int topCount)
        {
            try
            {
                var result = await _eventPopularityService.GetMostRealizableEvents(topCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving most realizable events: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("eventtypes/realizable/{topCount}")]
        public async Task<IActionResult> GetMostRealizableEventTypesAsync(int topCount)
        {
            try
            {
                var result = await _eventPopularityService.GetMostRealizableEventTypes(topCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving most realizable event types: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("events/popularity/export/excel")]
        public async Task<IActionResult> GetEventsPopularityByPeriodToExcelAsync(DateTime startDateTime, DateTime endDateTime)
        {
            try
            {
                var result = await _eventPopularityService.GetEventsPopularityByPeriod(startDateTime, endDateTime);

                var stream = await _excelFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.ExcelContentType, $"{EventsPopularityByPeriodFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving events popularity: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("eventtypes/statistic/{eventTypeId}/export/excel")]
        public async Task<IActionResult> GetEventTypeStatisticToExcelAsync(long eventTypeId)
        {
            try
            {
                var result = await _eventPopularityService.GetEventTypeStatistic(eventTypeId);

                var stream = await _excelFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.ExcelContentType, $"{EventTypePopularityFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving event type statistic: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("events/popular/{topCount}/export/excel")]
        public async Task<IActionResult> GetMostPopularEventsToExcelAsync(int topCount)
        {
            try
            {
                var result = await _eventPopularityService.GetMostPopularEvents(topCount);

                var stream = await _excelFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.ExcelContentType, $"{MostPopularEventsFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving most popular events: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("eventtypes/popular/{topCount}/export/excel")]
        public async Task<IActionResult> GetMostPopularEventTypesToExcelAsync(int topCount)
        {
            try
            {
                var result = await _eventPopularityService.GetMostPopularEventTypes(topCount);

                var stream = await _excelFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.ExcelContentType, $"{EventTypesPopularityFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving most popular event types: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("events/realizable/{topCount}/export/excel")]
        public async Task<IActionResult> GetMostRealizableEventsToExcelAsync(int topCount)
        {
            try
            {
                var result = await _eventPopularityService.GetMostRealizableEvents(topCount);

                var stream = await _excelFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.ExcelContentType, $"{MostRealizableEventsFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving most realizable events: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("eventtypes/realizable/{topCount}/export/excel")]
        public async Task<IActionResult> GetMostRealizableEventTypesToExcelAsync(int topCount)
        {
            try
            {
                var result = await _eventPopularityService.GetMostRealizableEventTypes(topCount);

                var stream = await _excelFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.ExcelContentType, $"{MostRealizableEventTypesFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving most realizable event types: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("events/popularity/export/csv")]
        public async Task<IActionResult> GetEventsPopularityByPeriodToCsvAsync(DateTime startDateTime, DateTime endDateTime)
        {
            try
            {
                var result = await _eventPopularityService.GetEventsPopularityByPeriod(startDateTime, endDateTime);

                var stream = await _csvFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.CsvContentType, $"{EventsPopularityByPeriodFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving events popularity: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("eventtypes/statistic/{eventTypeId}/export/csv")]
        public async Task<IActionResult> GetEventTypeStatisticToCsvAsync(long eventTypeId)
        {
            try
            {
                var result = await _eventPopularityService.GetEventTypeStatistic(eventTypeId);

                var stream = await _excelFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.CsvContentType, $"{EventTypePopularityFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving event type statistic: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("events/popular/{topCount}/export/csv")]
        public async Task<IActionResult> GetMostPopularEventsToCsvAsync(int topCount)
        {
            try
            {
                var result = await _eventPopularityService.GetMostPopularEvents(topCount);

                var stream = await _csvFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.CsvContentType, $"{MostPopularEventsFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving most popular events: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("eventtypes/popular/{topCount}/export/csv")]
        public async Task<IActionResult> GetMostPopularEventTypesToCsvAsync(int topCount)
        {
            try
            {
                var result = await _eventPopularityService.GetMostPopularEventTypes(topCount);

                var stream = await _csvFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.CsvContentType, $"{EventTypesPopularityFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving most popular event types: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("events/realizable/{topCount}/export/csv")]
        public async Task<IActionResult> GetMostRealizableEventsToCsvAsync(int topCount)
        {
            try
            {
                var result = await _eventPopularityService.GetMostRealizableEvents(topCount);

                var stream = await _csvFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.CsvContentType, $"{MostRealizableEventsFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving most realizable events: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("eventtypes/realizable/{topCount}/export/csv")]
        public async Task<IActionResult> GetMostRealizableEventTypesToCsvAsync(int topCount)
        {
            try
            {
                var result = await _eventPopularityService.GetMostRealizableEventTypes(topCount);

                var stream = await _csvFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.CsvContentType, $"{MostRealizableEventTypesFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving most realizable event types: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
