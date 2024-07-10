using EventSeller.Helpers;
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

        private const string TicketsStatisticsForEventFileName = "SalesStatisticsForEvent";
        private const string TicketsStatisticsForEventsFileName = "SalesStatisticsForEvents";
        private const string TicketsStatisticsForEventSessionFileName = "SalesStatisticsForEventSession";
        private const string TicketsStatisticsForEventAndSessionFileName = "SalesStatisticsForEventAndSession";
        private const string TicketsStatisticsForEventSessionsFileName = "SalesStatisticsForEventSessions";
        private const string TicketsStatisticsForPeriodFileName = "SalesStatisticsForTimePeriod";
        private const string TicketsStatisticsForEventTypeFileName = "SalesStatisticsForEventType";

        public SalesStatisticsController(ITicketSalesStatisticService ticketSalesStatisticService, ICsvFileExport csvFileExport, IExcelFileExport excelFileExport)
        {
            _ticketSalesStatisticService = ticketSalesStatisticService;
            _csvFileExport = csvFileExport;
            _excelFileExport = excelFileExport;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("tickets/statistics/event/{eventId}")]
        public async Task<IActionResult> GetSalesStatisticForEventAsync(long eventId)
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
        [HttpGet("tickets/statistics/events")]
        public async Task<IActionResult> GetSalesStatisticForEventsAsync([FromQuery] IEnumerable<long> eventIds)
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
        [HttpGet("tickets/statistics/event-session/{eventSessionId}")]
        public async Task<IActionResult> GetSalesStatisticForEventSessionAsync(long eventSessionId)
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
        [HttpGet("tickets/statistics/event/{eventId}/session/{eventSessionId}")]
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
        [HttpGet("tickets/statistics/event-sessions")]
        public async Task<IActionResult> GetSalesStatisticForEventSessionsAsync([FromQuery] IEnumerable<long> eventSessionIds)
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
        [HttpGet("tickets/statistics/event-type/{eventTypeId}")]
        public async Task<IActionResult> GetSalesStatisticForEventTypeAsync(long eventTypeId)
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
        [HttpGet("tickets/statistics/period")]
        public async Task<IActionResult> GetSalesStatisticForPeriodAsync([FromQuery] DateTime firstPeriod, [FromQuery] DateTime secondPeriod)
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
        [HttpGet("tickets/statistics/week")]
        public async Task<IActionResult> GetSalesStatisticForWeekAsync([FromQuery] DateTime weekStartDate)
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
        [HttpGet("tickets/statistics/day")]
        public async Task<IActionResult> GetSalesStatisticForDayAsync([FromQuery] DateTime dayDate)
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
        // sdasdasdasd
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("tickets/statistics/event/{eventId}/export/excel")]
        public async Task<IActionResult> GetSalesStatisticForEventToExcelAsync(long eventId)
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForEventAsync(eventId);

              var stream = await _excelFileExport.ExportFileAsync(statistics);
              return File(stream, HelperConstants.ExcelContentType, $"{TicketsStatisticsForEventFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving sales statistics for event {eventId}: {ex.Message}");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("tickets/statistics/events/export/excel")]
        public async Task<IActionResult> GetSalesStatisticForEventsToExcelAsync([FromQuery] IEnumerable<long> eventIds)
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForEventsAsync(eventIds);

                var stream = await _excelFileExport.ExportFileAsync(statistics);
                return File(stream, HelperConstants.ExcelContentType, $"{TicketsStatisticsForEventsFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving sales statistics for events: {ex.Message}");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("tickets/statistics/event-session/{eventSessionId}/export/excel")]
        public async Task<IActionResult> GetSalesStatisticForEventSessionToExcelAsync(long eventSessionId)
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForEventSessionAsync(eventSessionId);

                var stream = await _excelFileExport.ExportFileAsync(statistics);
                return File(stream, HelperConstants.ExcelContentType, $"{TicketsStatisticsForEventSessionFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving sales statistics for event session {eventSessionId}: {ex.Message}");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("tickets/statistics/event/{eventId}/session/{eventSessionId}/export/excel")]
        public async Task<IActionResult> GetSalesStatisticForEventAndSessionToExcelAsync(long eventId, long eventSessionId)
        {
            try
            {
                var result = await _ticketSalesStatisticService.GetSalesStatisticForEventAndSessionAsync(eventId, eventSessionId);

                var stream = await _excelFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.ExcelContentType, $"{TicketsStatisticsForEventAndSessionFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("tickets/statistics/event-sessions/export/excel")]
        public async Task<IActionResult> GetSalesStatisticForEventSessionsToExcelAsync([FromQuery] IEnumerable<long> eventSessionIds)
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForEventSessionsAsync(eventSessionIds);

                var stream = await _excelFileExport.ExportFileAsync(statistics);
                return File(stream, HelperConstants.ExcelContentType, $"{TicketsStatisticsForEventSessionsFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving sales statistics for event sessions: {ex.Message}");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("tickets/statistics/event-type/{eventTypeId}/export/excel")]
        public async Task<IActionResult> GetSalesStatisticForEventTypeToExcelAsync(long eventTypeId)
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForEventTypeAsync(eventTypeId);

                var stream = await _excelFileExport.ExportFileAsync(statistics);
                return File(stream, HelperConstants.ExcelContentType, $"{TicketsStatisticsForEventTypeFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving sales statistics for event type {eventTypeId}: {ex.Message}");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("tickets/statistics/period/export/excel")]
        public async Task<IActionResult> GetSalesStatisticForPeriodToExcelAsync([FromQuery] DateTime firstPeriod, [FromQuery] DateTime secondPeriod)
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForPeriodAsync(firstPeriod, secondPeriod);

                var stream = await _excelFileExport.ExportFileAsync(statistics);
                return File(stream, HelperConstants.ExcelContentType, $"{TicketsStatisticsForPeriodFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving sales statistics for period {firstPeriod} - {secondPeriod}: {ex.Message}");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("tickets/statistics/week/export/excel")]
        public async Task<IActionResult> GetSalesStatisticForWeekToExcelAsync([FromQuery] DateTime weekStartDate)
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForWeekAsync(weekStartDate);

                var stream = await _excelFileExport.ExportFileAsync(statistics);
                return File(stream, HelperConstants.ExcelContentType, $"{TicketsStatisticsForPeriodFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving sales statistics for week starting on {weekStartDate}: {ex.Message}");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("tickets/statistics/day/export/excel")]
        public async Task<IActionResult> GetSalesStatisticForDayToExcelAsync([FromQuery] DateTime dayDate)
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForDayAsync(dayDate);

                var stream = await _excelFileExport.ExportFileAsync(statistics);
                return File(stream, HelperConstants.ExcelContentType, $"{TicketsStatisticsForPeriodFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving sales statistics for day {dayDate}: {ex.Message}");
            }
        }
        /// csv
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("tickets/statistics/event/{eventId}/export/csv")]
        public async Task<IActionResult> GetSalesStatisticForEventToCsvAsync(long eventId)
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForEventAsync(eventId);

                var stream = await _csvFileExport.ExportFileAsync(statistics);
                return File(stream, HelperConstants.CsvContentType, $"{TicketsStatisticsForEventFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving sales statistics for event {eventId}: {ex.Message}");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("tickets/statistics/events/export/csv")]
        public async Task<IActionResult> GetSalesStatisticForEventsToCsvAsync([FromQuery] IEnumerable<long> eventIds)
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForEventsAsync(eventIds);

                var stream = await _csvFileExport.ExportFileAsync(statistics);
                return File(stream, HelperConstants.CsvContentType, $"{TicketsStatisticsForEventsFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving sales statistics for events: {ex.Message}");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("tickets/statistics/event-session/{eventSessionId}/export/csv")]
        public async Task<IActionResult> GetSalesStatisticForEventSessionToCsvAsync(long eventSessionId)
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForEventSessionAsync(eventSessionId);

                var stream = await _csvFileExport.ExportFileAsync(statistics);
                return File(stream, HelperConstants.CsvContentType, $"{TicketsStatisticsForEventSessionFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving sales statistics for event session {eventSessionId}: {ex.Message}");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("tickets/statistics/event/{eventId}/session/{eventSessionId}/export/csv")]
        public async Task<IActionResult> GetSalesStatisticForEventAndSessionToCsvAsync(long eventId, long eventSessionId)
        {
            try
            {
                var result = await _ticketSalesStatisticService.GetSalesStatisticForEventAndSessionAsync(eventId, eventSessionId);

                var stream = await _csvFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.CsvContentType, $"{TicketsStatisticsForEventAndSessionFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("tickets/statistics/event-sessions/export/csv")]
        public async Task<IActionResult> GetSalesStatisticForEventSessionsToCsvAsync([FromQuery] IEnumerable<long> eventSessionIds)
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForEventSessionsAsync(eventSessionIds);

                var stream = await _csvFileExport.ExportFileAsync(statistics);
                return File(stream, HelperConstants.CsvContentType, $"{TicketsStatisticsForEventSessionsFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving sales statistics for event sessions: {ex.Message}");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("tickets/statistics/event-type/{eventTypeId}/export/csv")]
        public async Task<IActionResult> GetSalesStatisticForEventTypeToCsvAsync(long eventTypeId)
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForEventTypeAsync(eventTypeId);

                var stream = await _csvFileExport.ExportFileAsync(statistics);
                return File(stream, HelperConstants.CsvContentType, $"{TicketsStatisticsForEventTypeFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving sales statistics for event type {eventTypeId}: {ex.Message}");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("tickets/statistics/period/export/csv")]
        public async Task<IActionResult> GetSalesStatisticForPeriodToCsvAsync([FromQuery] DateTime firstPeriod, [FromQuery] DateTime secondPeriod)
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForPeriodAsync(firstPeriod, secondPeriod);

                var stream = await _csvFileExport.ExportFileAsync(statistics);
                return File(stream, HelperConstants.CsvContentType, $"{TicketsStatisticsForPeriodFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving sales statistics for period {firstPeriod} - {secondPeriod}: {ex.Message}");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("tickets/statistics/week/export/csv")]
        public async Task<IActionResult> GetSalesStatisticForWeekToCsvAsync([FromQuery] DateTime weekStartDate)
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForWeekAsync(weekStartDate);

                var stream = await _csvFileExport.ExportFileAsync(statistics);
                return File(stream, HelperConstants.CsvContentType, $"{TicketsStatisticsForPeriodFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving sales statistics for week starting on {weekStartDate}: {ex.Message}");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("tickets/statistics/day/export/csv")]
        public async Task<IActionResult> GetSalesStatisticForDayToCsvAsync([FromQuery] DateTime dayDate)
        {
            try
            {
                var statistics = await _ticketSalesStatisticService.GetSalesStatisticForDayAsync(dayDate);

                var stream = await _csvFileExport.ExportFileAsync(statistics);
                return File(stream, HelperConstants.CsvContentType, $"{TicketsStatisticsForPeriodFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving sales statistics for day {dayDate}: {ex.Message}");
            }
        }
    }
}
