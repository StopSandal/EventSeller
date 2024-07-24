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
        private readonly IExcelFileExport _excelFileExport;
        private readonly ICsvFileExport _csvFileExport;
        private readonly ILogger<SalesStatisticsController> _logger;

        private const string TicketsStatisticsForEventFileName = "SalesStatisticsForEvent";
        private const string TicketsStatisticsForEventsFileName = "SalesStatisticsForEvents";
        private const string TicketsStatisticsForEventSessionFileName = "SalesStatisticsForEventSession";
        private const string TicketsStatisticsForEventAndSessionFileName = "SalesStatisticsForEventAndSession";
        private const string TicketsStatisticsForEventSessionsFileName = "SalesStatisticsForEventSessions";
        private const string TicketsStatisticsForPeriodFileName = "SalesStatisticsForTimePeriod";
        private const string TicketsStatisticsForEventTypeFileName = "SalesStatisticsForEventType";

        public SalesStatisticsController(ITicketSalesStatisticService ticketSalesStatisticService, ICsvFileExport csvFileExport, IExcelFileExport excelFileExport, ILogger<SalesStatisticsController> logger)
        {
            _ticketSalesStatisticService = ticketSalesStatisticService;
            _csvFileExport = csvFileExport;
            _excelFileExport = excelFileExport;
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

                switch (format.ToLower())
                {
                    case "excel":
                        var excelStream = await _excelFileExport.ExportFileAsync(statistics);
                        return File(excelStream, HelperConstants.ExcelContentType, $"{TicketsStatisticsForEventFileName}{HelperConstants.ExcelExtension}");

                    case "csv":
                        var csvStream = await _csvFileExport.ExportFileAsync(statistics);
                        return File(csvStream, HelperConstants.CsvContentType, $"{TicketsStatisticsForEventFileName}{HelperConstants.CsvExtension}");

                    case "json":
                        return Ok(statistics);

                    default:
                        return BadRequest("Invalid format specified. Supported formats are 'json', 'excel', and 'csv'.");
                }
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

                switch (format.ToLower())
                {
                    case "excel":
                        var excelStream = await _excelFileExport.ExportFileAsync(statistics);
                        return File(excelStream, HelperConstants.ExcelContentType, $"{TicketsStatisticsForEventsFileName}{HelperConstants.ExcelExtension}");

                    case "csv":
                        var csvStream = await _csvFileExport.ExportFileAsync(statistics);
                        return File(csvStream, HelperConstants.CsvContentType, $"{TicketsStatisticsForEventsFileName}{HelperConstants.CsvExtension}");

                    case "json":
                        return Ok(statistics);

                    default:
                        return BadRequest("Invalid format specified. Supported formats are 'json', 'excel', and 'csv'.");
                }
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

                switch (format.ToLower())
                {
                    case "excel":
                        var excelStream = await _excelFileExport.ExportFileAsync(statistics);
                        return File(excelStream, HelperConstants.ExcelContentType, $"{TicketsStatisticsForEventSessionFileName}{HelperConstants.ExcelExtension}");

                    case "csv":
                        var csvStream = await _csvFileExport.ExportFileAsync(statistics);
                        return File(csvStream, HelperConstants.CsvContentType, $"{TicketsStatisticsForEventSessionFileName}{HelperConstants.CsvExtension}");

                    case "json":
                        return Ok(statistics);

                    default:
                        return BadRequest("Invalid format specified. Supported formats are 'json', 'excel', and 'csv'.");
                }
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

                switch (format.ToLower())
                {
                    case "excel":
                        var excelStream = await _excelFileExport.ExportFileAsync(result);
                        return File(excelStream, HelperConstants.ExcelContentType, $"{TicketsStatisticsForEventAndSessionFileName}{HelperConstants.ExcelExtension}");

                    case "csv":
                        var csvStream = await _csvFileExport.ExportFileAsync(result);
                        return File(csvStream, HelperConstants.CsvContentType, $"{TicketsStatisticsForEventAndSessionFileName}{HelperConstants.CsvExtension}");

                    case "json":
                        return Ok(result);

                    default:
                        return BadRequest("Invalid format specified. Supported formats are 'json', 'excel', and 'csv'.");
                }
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

                switch (format.ToLower())
                {
                    case "excel":
                        var excelStream = await _excelFileExport.ExportFileAsync(statistics);
                        return File(excelStream, HelperConstants.ExcelContentType, $"{TicketsStatisticsForEventSessionsFileName}{HelperConstants.ExcelExtension}");

                    case "csv":
                        var csvStream = await _csvFileExport.ExportFileAsync(statistics);
                        return File(csvStream, HelperConstants.CsvContentType, $"{TicketsStatisticsForEventSessionsFileName}{HelperConstants.CsvExtension}");

                    case "json":
                        return Ok(statistics);

                    default:
                        return BadRequest("Invalid format specified. Supported formats are 'json', 'excel', and 'csv'.");
                }
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

                switch (format.ToLower())
                {
                    case "excel":
                        var excelStream = await _excelFileExport.ExportFileAsync(statistics);
                        return File(excelStream, HelperConstants.ExcelContentType, $"{TicketsStatisticsForEventTypeFileName}{HelperConstants.ExcelExtension}");

                    case "csv":
                        var csvStream = await _csvFileExport.ExportFileAsync(statistics);
                        return File(csvStream, HelperConstants.CsvContentType, $"{TicketsStatisticsForEventTypeFileName}{HelperConstants.CsvExtension}");

                    case "json":
                        return Ok(statistics);

                    default:
                        return BadRequest("Invalid format specified. Supported formats are 'json', 'excel', and 'csv'.");
                }
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

                switch (format.ToLower())
                {
                    case "excel":
                        var excelStream = await _excelFileExport.ExportFileAsync(statistics);
                        return File(excelStream, HelperConstants.ExcelContentType, $"{TicketsStatisticsForPeriodFileName}{HelperConstants.ExcelExtension}");

                    case "csv":
                        var csvStream = await _csvFileExport.ExportFileAsync(statistics);
                        return File(csvStream, HelperConstants.CsvContentType, $"{TicketsStatisticsForPeriodFileName}{HelperConstants.CsvExtension}");

                    case "json":
                        return Ok(statistics);

                    default:
                        return BadRequest("Invalid format specified. Supported formats are 'json', 'excel', and 'csv'.");
                }
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

                switch (format.ToLower())
                {
                    case "excel":
                        var excelStream = await _excelFileExport.ExportFileAsync(statistics);
                        return File(excelStream, HelperConstants.ExcelContentType, $"{TicketsStatisticsForPeriodFileName}{HelperConstants.ExcelExtension}");

                    case "csv":
                        var csvStream = await _csvFileExport.ExportFileAsync(statistics);
                        return File(csvStream, HelperConstants.CsvContentType, $"{TicketsStatisticsForPeriodFileName}{HelperConstants.CsvExtension}");

                    case "json":
                        return Ok(statistics);

                    default:
                        return BadRequest("Invalid format specified. Supported formats are 'json', 'excel', and 'csv'.");
                }
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

                switch (format.ToLower())
                {
                    case "excel":
                        var excelStream = await _excelFileExport.ExportFileAsync(statistics);
                        return File(excelStream, HelperConstants.ExcelContentType, $"{TicketsStatisticsForPeriodFileName}{HelperConstants.ExcelExtension}");

                    case "csv":
                        var csvStream = await _csvFileExport.ExportFileAsync(statistics);
                        return File(csvStream, HelperConstants.CsvContentType, $"{TicketsStatisticsForPeriodFileName}{HelperConstants.CsvExtension}");

                    case "json":
                        return Ok(statistics);

                    default:
                        return BadRequest("Invalid format specified. Supported formats are 'json', 'excel', and 'csv'.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving sales statistics for day {dayDate}: {ex.Message}");
                return BadRequest($"Error retrieving sales statistics for day {dayDate}: {ex.Message}");
            }
        }
    }
}
