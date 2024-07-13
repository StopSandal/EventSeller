using EventSeller.Helpers;
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
        private readonly IExcelFileExport _excelFileExport;
        private readonly ICsvFileExport _csvFileExport;
        private const string HallGroupedByEventFileName = "SectorsPopularityByHallGrouped";
        private const string HallFileName = "SectorsPopularityByHall";
        private const string ForEventFileName = "SectorsPopularityForEvent";

        public SectorsStatisticsController(ISectorsStatisticsService sectorsStatisticsService, IExcelFileExport excelFileExport, ICsvFileExport csvFileExport)
        {
            _sectorsPopularityService = sectorsStatisticsService;
            _excelFileExport = excelFileExport;
            _csvFileExport = csvFileExport;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("sectors/popularity/event/{eventId}")]
        public async Task<IActionResult> GetSectorsPopularityForEventAsync(long eventId, [FromQuery] int maxCount = 0)
        {
            try
            {
                var result = await _sectorsPopularityService.GetSectorsPopularityForEventAsync(eventId, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("sectors/popularity/hall/{placeHallId}")]
        public async Task<IActionResult> GetSectorsPopularityInHallAsync(long placeHallId, [FromQuery] int maxCount = 0)
        {
            try
            {
                var result = await _sectorsPopularityService.GetSectorsPopularityInHallAsync(placeHallId, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("sectors/popularity/by-groups/hall/{placeHallId}")]
        public async Task<IActionResult> GetSectorsPopularityByEventGroupsAtHallAsync(long placeHallId, [FromQuery] IEnumerable<long> eventIds, int maxCount = 0)
        {
            try
            {
                var result = await _sectorsPopularityService.GetSectorsPopularityByEventGroupsAtHallAsync(placeHallId, eventIds, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("sectors/popularity/event/{eventId}/export/excel")]
        public async Task<IActionResult> GetSectorsPopularityForEventToExcelAsync(long eventId, [FromQuery] int maxCount = 0)
        {
            try
            {
                var result = await _sectorsPopularityService.GetSectorsPopularityForEventAsync(eventId, maxCount);
                var stream = await _excelFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.ExcelContentType, $"{ForEventFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("sectors/popularity/hall/{placeHallId}/export/excel")]
        public async Task<IActionResult> GetSectorsPopularityInHallToExcelAsync(long placeHallId, [FromQuery] int maxCount = 0)
        {
            try
            {
                var result = await _sectorsPopularityService.GetSectorsPopularityInHallAsync(placeHallId, maxCount);
                var stream = await _excelFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.ExcelContentType, $"{HallFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("sectors/popularity/by-groups/hall/{placeHallId}/export/excel")]
        public async Task<IActionResult> GetSectorsPopularityByEventGroupsAtHallToExcel(long placeHallId, [FromQuery] IEnumerable<long> eventIds, int maxCount = 0)
        {
            try
            {
                var result = await _sectorsPopularityService.GetSectorsPopularityByEventGroupsAtHallAsync(placeHallId, eventIds, maxCount);

                var stream = await _excelFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.ExcelContentType, $"{HallGroupedByEventFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("sectors/popularity/event/{eventId}/export/csv")]
        public async Task<IActionResult> GetSectorsPopularityForEventToCsvAsync(long eventId, [FromQuery] int maxCount = 0)
        {
            try
            {
                var result = await _sectorsPopularityService.GetSectorsPopularityForEventAsync(eventId, maxCount);
                var stream = await _csvFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.CsvContentType, $"{ForEventFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("sectors/popularity/hall/{placeHallId}/export/csv")]
        public async Task<IActionResult> GetSectorsPopularityInHallToCsvAsync(long placeHallId, [FromQuery] int maxCount = 0)
        {
            try
            {
                var result = await _sectorsPopularityService.GetSectorsPopularityInHallAsync(placeHallId, maxCount);
                var stream = await _csvFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.CsvContentType, $"{HallFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("sectors/popularity/by-groups/hall/{placeHallId}/export/csv")]
        public async Task<IActionResult> GetSectorsPopularityByEventGroupsAtHallToCsvAsync(long placeHallId, [FromQuery] IEnumerable<long> eventIds, int maxCount = 0)
        {
            try
            {
                var result = await _sectorsPopularityService.GetSectorsPopularityByEventGroupsAtHallAsync(placeHallId, eventIds, maxCount);

                var stream = await _csvFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.CsvContentType, $"{HallGroupedByEventFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
