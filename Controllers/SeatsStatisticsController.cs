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

        private const string HallGroupedByEventFileName = "SeatsPopularityByHallGrouped";
        private const string HallFileName = "SeatsPopularityByHall";
        private const string ForEventFileName = "SeatsPopularityForEvent";
        public SeatsStatisticsController(ISeatsPopularityService seatsPopularityService, IExcelFileExport excelFileExport, ICsvFileExport csvFileExport)
        {
            _seatsPopularityService = seatsPopularityService;
            _excelFileExport = excelFileExport;
            _csvFileExport = csvFileExport;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("seats/popularity/event/{eventId}")]
        public async Task<IActionResult> GetSeatsPopularityForEventAsync(long eventId, [FromQuery] int maxCount = 0)
        {
            try
            {
                var result = await _seatsPopularityService.GetSeatsPopularityForEventAsync(eventId, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("seats/popularity/hall/{placeHallId}")]
        public async Task<IActionResult> GetSeatsPopularityInHallAsync(long placeHallId, [FromQuery] int maxCount = 0)
        {
            try
            {
                var result = await _seatsPopularityService.GetSeatsPopularityInHallAsync(placeHallId, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("seats/popularity/by-groups/hall/{placeHallId}")]
        public async Task<IActionResult> GetSeatsPopularityByEventGroupsAtHallAsync(long placeHallId, [FromQuery] IEnumerable<long> eventIds, int maxCount = 0)
        {
            try
            {
                var result = await _seatsPopularityService.GetSeatsPopularityByEventGroupsAtHallAsync(placeHallId, eventIds, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("seats/popularity/by-groups/hall/{placeHallId}/export/excel")]
        public async Task<IActionResult> GetSeatsPopularityByEventGroupsAtHallToExcelAsync(long placeHallId, [FromQuery] IEnumerable<long> eventIds, int maxCount = 0)
        {
            try
            {
                var result = await _seatsPopularityService.GetSeatsPopularityByEventGroupsAtHallAsync(placeHallId, eventIds, maxCount);
                var stream = await _excelFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.ExcelContentType, $"{HallGroupedByEventFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("seats/popularity/event/{eventId}/export/excel")]
        public async Task<IActionResult> GetSeatsPopularityForEventToExcelAsync(long eventId, [FromQuery] int maxCount = 0)
        {
            try
            {
                var result = await _seatsPopularityService.GetSeatsPopularityForEventAsync(eventId, maxCount);
                var stream = await _excelFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.ExcelContentType, $"{ForEventFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("seats/popularity/hall/{placeHallId}/export/excel")]
        public async Task<IActionResult> GetSeatsPopularityInHallToExcelAsync(long placeHallId, [FromQuery] int maxCount = 0)
        {
            try
            {
                var result = await _seatsPopularityService.GetSeatsPopularityInHallAsync(placeHallId, maxCount);

                var stream = await _excelFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.ExcelContentType, $"{HallFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("seats/popularity/by-groups/hall/{placeHallId}/export/csv")]
        public async Task<IActionResult> GetSeatsPopularityByEventGroupsAtHallToCsvAsync(long placeHallId, [FromQuery] IEnumerable<long> eventIds, int maxCount = 0)
        {
            try
            {
                var result = await _seatsPopularityService.GetSeatsPopularityByEventGroupsAtHallAsync(placeHallId, eventIds, maxCount);
                var stream = await _csvFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.CsvContentType, $"{HallGroupedByEventFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("seats/popularity/event/{eventId}/export/csv")]
        public async Task<IActionResult> GetSeatsPopularityForEventToCsvAsync(long eventId, [FromQuery] int maxCount = 0)
        {
            try
            {
                var result = await _seatsPopularityService.GetSeatsPopularityForEventAsync(eventId, maxCount);
                var stream = await _csvFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.CsvContentType, $"{ForEventFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("seats/popularity/hall/{placeHallId}/export/csv")]
        public async Task<IActionResult> GetSeatsPopularityInHallToCsvAsync(long placeHallId, [FromQuery] int maxCount = 0)
        {
            try
            {
                var result = await _seatsPopularityService.GetSeatsPopularityInHallAsync(placeHallId, maxCount);

                var stream = await _csvFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.CsvContentType, $"{HallFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
