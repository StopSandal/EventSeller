using EventSeller.DataLayer.EntitiesDto.Statistics;
using EventSeller.Helpers;
using EventSeller.Services.Interfaces.Exporters;
using EventSeller.Services.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace EventSeller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DaysTrafficController : ControllerBase
    {
        private readonly IDayTrafficStatisticService _dayTrafficStatisticService;
        private readonly IExcelFileExport _excelFileExport;
        private readonly ICsvFileExport _csvFileExport;
        private readonly ILogger<DaysTrafficController> _logger;

        private const string TrafficByDaysFileName = "TrafficStatisticsByDays";
        private const string TrafficByTrafficFileName = "TrafficStatisticsByDays";
        private const string TrafficForPeriodByDaysFileName = "TrafficStatisticsForPeriodByDays";
        private const string TrafficForPeriodByTrafficFileName = "TrafficStatisticsForPeriodByTraffic";
        private const string TrafficForHallByDaysFileName = "TrafficStatisticsForHallByDays";
        private const string TrafficForHallByTrafficFileName = "TrafficStatisticsForHallByTraffic";
        private const string TrafficForPlaceByDaysFileName = "TrafficStatisticsForPlaceByDays";
        private const string TrafficForPlaceByTrafficFileName = "TrafficStatisticsForPlaceByTraffic";

        public DaysTrafficController(IDayTrafficStatisticService dayTrafficStatisticService, ILogger<DaysTrafficController> logger, ICsvFileExport csvFileExport, IExcelFileExport excelFileExport)
        {
            _dayTrafficStatisticService = dayTrafficStatisticService;
            _logger = logger;
            _csvFileExport = csvFileExport;
            _excelFileExport = excelFileExport;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("traffic/byDate")]
        public async Task<IActionResult> GetDaysTrafficOrderedByDayAsync([FromQuery] int maxCount = 0)
        {
            _logger.LogInformation("Getting days traffic statistics ordered by day.");

            try
            {
                Expression<Func<DaysStatistics, DateTime>> orderByDay = x => x.Date;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAsync(orderByDay, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics ordered by day.");
                return BadRequest("An error occurred while getting days traffic statistics ordered by day.");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("traffic/byTraffic")]
        public async Task<IActionResult> GetDaysTrafficOrderedByTotalTrafficAsync([FromQuery] int maxCount = 0)
        {
            _logger.LogInformation("Getting days traffic statistics ordered by total traffic.");

            try
            {
                Expression<Func<DaysStatistics, int>> orderByTotalTraffic = x => x.TotalTraffic;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAsync(orderByTotalTraffic, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics ordered by total traffic.");
                return BadRequest("An error occurred while getting days traffic statistics ordered by total traffic.");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("traffic/ForPeriod/ByDay")]
        public async Task<IActionResult> GetDaysTrafficAtPeriodOrderedByDayAsync([FromQuery] DateTime startPeriod, [FromQuery] DateTime endPeriod, [FromQuery] int maxCount = 0)
        {
            _logger.LogInformation("Getting days traffic statistics at period ordered by day.");

            try
            {
                Expression<Func<DaysStatistics, DateTime>> orderByDay = x => x.Date;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAtPeriodAsync(startPeriod, endPeriod, orderByDay, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics at period ordered by day.");
                return BadRequest("An error occurred while getting days traffic statistics at period ordered by day.");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("traffic/ForPeriod/ByTraffic")]
        public async Task<IActionResult> GetDaysTrafficAtPeriodOrderedByTotalTrafficAsync([FromQuery] DateTime startPeriod, [FromQuery] DateTime endPeriod, [FromQuery] int maxCount = 0)
        {
            _logger.LogInformation("Getting days traffic statistics at period ordered by total traffic.");

            try
            {
                Expression<Func<DaysStatistics, int>> orderByTotalTraffic = x => x.TotalTraffic;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAtPeriodAsync(startPeriod, endPeriod, orderByTotalTraffic, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics at period ordered by total traffic.");
                return BadRequest("An error occurred while getting days traffic statistics at period ordered by total traffic.");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("traffic/ForHall/ByDay")]
        public async Task<IActionResult> GetDaysTrafficAtHallOrderedByDayAsync([FromQuery] long placeHallId, [FromQuery] int maxCount = 0)
        {
            _logger.LogInformation("Getting days traffic statistics at hall ordered by day.");

            try
            {
                Expression<Func<DaysStatistics, DateTime>> orderByDay = x => x.Date;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAtHallAsync(placeHallId, orderByDay, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics at hall ordered by day.");
                return BadRequest("An error occurred while getting days traffic statistics at hall ordered by day.");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("traffic/ForHall/ByTraffic")]
        public async Task<IActionResult> GetDaysTrafficAtHallOrderedByTotalTrafficAsync([FromQuery] long placeHallId, [FromQuery] int maxCount = 0)
        {
            _logger.LogInformation("Getting days traffic statistics at hall ordered by total traffic.");

            try
            {
                Expression<Func<DaysStatistics, int>> orderByTotalTraffic = x => x.TotalTraffic;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAtHallAsync(placeHallId, orderByTotalTraffic, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics at hall ordered by total traffic.");
                return BadRequest("An error occurred while getting days traffic statistics at hall ordered by total traffic.");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("traffic/ForPlace/ByDay")]
        public async Task<IActionResult> GetDaysTrafficAtPlaceOrderedByDayAsync([FromQuery] long placeAddressId, [FromQuery] int maxCount = 0)
        {
            _logger.LogInformation("Getting days traffic statistics at place ordered by day.");

            try
            {
                Expression<Func<DaysStatistics, DateTime>> orderByDay = x => x.Date;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAtPlaceAsync(placeAddressId, orderByDay, maxCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics at place ordered by day.");
                return BadRequest("An error occurred while getting days traffic statistics at place ordered by day.");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("traffic/ForPlace/ByTraffic")]
        public async Task<IActionResult> GetDaysTrafficAtPlaceOrderedByTotalTrafficAsync([FromQuery] long placeAddressId, [FromQuery] int maxCount = 0)
        {
            _logger.LogInformation("Getting days traffic statistics at place ordered by total traffic.");

            try
            {
                Expression<Func<DaysStatistics, int>> orderByTotalTraffic = x => x.TotalTraffic;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAtPlaceAsync(placeAddressId, orderByTotalTraffic, maxCount);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics at place ordered by total traffic.");
                return BadRequest("An error occurred while getting days traffic statistics at place ordered by total traffic.");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("traffic/byDate/export/excel")]
        public async Task<IActionResult> GetDaysTrafficOrderedByDayToExcelAsync([FromQuery] int maxCount = 0)
        {
            _logger.LogInformation("Getting days traffic statistics ordered by day.");

            try
            {
                Expression<Func<DaysStatistics, DateTime>> orderByDay = x => x.Date;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAsync(orderByDay, maxCount);

                var stream = await _excelFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.ExcelContentType, $"{TrafficByDaysFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics ordered by day.");
                return BadRequest("An error occurred while getting days traffic statistics ordered by day.");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("traffic/byTraffic/export/excel")]
        public async Task<IActionResult> GetDaysTrafficOrderedByTotalTrafficToExcelAsync([FromQuery] int maxCount = 0)
        {
            _logger.LogInformation("Getting days traffic statistics ordered by total traffic.");

            try
            {
                Expression<Func<DaysStatistics, int>> orderByTotalTraffic = x => x.TotalTraffic;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAsync(orderByTotalTraffic, maxCount);

                var stream = await _excelFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.ExcelContentType, $"{TrafficByTrafficFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics ordered by total traffic.");
                return BadRequest("An error occurred while getting days traffic statistics ordered by total traffic.");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("traffic/ForPeriod/ByDay/export/excel")]
        public async Task<IActionResult> GetDaysTrafficAtPeriodOrderedByDayToExcelAsync([FromQuery] DateTime startPeriod, [FromQuery] DateTime endPeriod, [FromQuery] int maxCount = 0)
        {
            _logger.LogInformation("Getting days traffic statistics at period ordered by day.");

            try
            {
                Expression<Func<DaysStatistics, DateTime>> orderByDay = x => x.Date;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAtPeriodAsync(startPeriod, endPeriod, orderByDay, maxCount);

                var stream = await _excelFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.ExcelContentType, $"{TrafficForPeriodByDaysFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics at period ordered by day.");
                return BadRequest("An error occurred while getting days traffic statistics at period ordered by day.");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("traffic/ForPeriod/ByTraffic/export/excel")]
        public async Task<IActionResult> GetDaysTrafficAtPeriodOrderedByTotalTrafficToExcelAsync([FromQuery] DateTime startPeriod, [FromQuery] DateTime endPeriod, [FromQuery] int maxCount = 0)
        {
            _logger.LogInformation("Getting days traffic statistics at period ordered by total traffic.");

            try
            {
                Expression<Func<DaysStatistics, int>> orderByTotalTraffic = x => x.TotalTraffic;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAtPeriodAsync(startPeriod, endPeriod, orderByTotalTraffic, maxCount);

                var stream = await _excelFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.ExcelContentType, $"{TrafficForPeriodByTrafficFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics at period ordered by total traffic.");
                return BadRequest("An error occurred while getting days traffic statistics at period ordered by total traffic.");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("traffic/ForHall/ByDay/export/excel")]
        public async Task<IActionResult> GetDaysTrafficAtHallOrderedByDayToExcelAsync([FromQuery] long placeHallId, [FromQuery] int maxCount = 0)
        {
            _logger.LogInformation("Getting days traffic statistics at hall ordered by day.");

            try
            {
                Expression<Func<DaysStatistics, DateTime>> orderByDay = x => x.Date;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAtHallAsync(placeHallId, orderByDay, maxCount);

                var stream = await _excelFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.ExcelContentType, $"{TrafficForHallByDaysFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics at hall ordered by day.");
                return BadRequest("An error occurred while getting days traffic statistics at hall ordered by day.");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("traffic/ForHall/ByTraffic/export/excel")]
        public async Task<IActionResult> GetDaysTrafficAtHallOrderedByTotalTrafficToExcelAsync([FromQuery] long placeHallId, [FromQuery] int maxCount = 0)
        {
            _logger.LogInformation("Getting days traffic statistics at hall ordered by total traffic.");

            try
            {
                Expression<Func<DaysStatistics, int>> orderByTotalTraffic = x => x.TotalTraffic;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAtHallAsync(placeHallId, orderByTotalTraffic, maxCount);

                var stream = await _excelFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.ExcelContentType, $"{TrafficForHallByTrafficFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics at hall ordered by total traffic.");
                return BadRequest("An error occurred while getting days traffic statistics at hall ordered by total traffic.");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("traffic/ForPlace/ByDay/export/excel")]
        public async Task<IActionResult> GetDaysTrafficAtPlaceOrderedByDayToExcelAsync([FromQuery] long placeAddressId, [FromQuery] int maxCount = 0)
        {
            _logger.LogInformation("Getting days traffic statistics at place ordered by day.");

            try
            {
                Expression<Func<DaysStatistics, DateTime>> orderByDay = x => x.Date;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAtPlaceAsync(placeAddressId, orderByDay, maxCount);

                var stream = await _excelFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.ExcelContentType, $"{TrafficForPlaceByDaysFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics at place ordered by day.");
                return BadRequest("An error occurred while getting days traffic statistics at place ordered by day.");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("traffic/ForPlace/ByTraffic/export/excel")]
        public async Task<IActionResult> GetDaysTrafficAtPlaceOrderedByTotalTrafficToExcelAsync([FromQuery] long placeAddressId, [FromQuery] int maxCount = 0)
        {
            _logger.LogInformation("Getting days traffic statistics at place ordered by total traffic.");

            try
            {
                Expression<Func<DaysStatistics, int>> orderByTotalTraffic = x => x.TotalTraffic;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAtPlaceAsync(placeAddressId, orderByTotalTraffic, maxCount);

                var stream = await _excelFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.ExcelContentType, $"{TrafficForPlaceByTrafficFileName}{HelperConstants.ExcelExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics at place ordered by total traffic.");
                return BadRequest("An error occurred while getting days traffic statistics at place ordered by total traffic.");
            }
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("traffic/byDate/export/csv")]
        public async Task<IActionResult> GetDaysTrafficOrderedByDayToCsvAsync([FromQuery] int maxCount = 0)
        {
            _logger.LogInformation("Getting days traffic statistics ordered by day.");

            try
            {
                Expression<Func<DaysStatistics, DateTime>> orderByDay = x => x.Date;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAsync(orderByDay, maxCount);

                var stream = await _csvFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.CsvContentType, $"{TrafficByDaysFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics ordered by day.");
                return BadRequest("An error occurred while getting days traffic statistics ordered by day.");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("traffic/byTraffic/export/csv")]
        public async Task<IActionResult> GetDaysTrafficOrderedByTotalTrafficToCsvAsync([FromQuery] int maxCount = 0)
        {
            _logger.LogInformation("Getting days traffic statistics ordered by total traffic.");

            try
            {
                Expression<Func<DaysStatistics, int>> orderByTotalTraffic = x => x.TotalTraffic;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAsync(orderByTotalTraffic, maxCount);

                var stream = await _csvFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.CsvContentType, $"{TrafficByTrafficFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics ordered by total traffic.");
                return BadRequest("An error occurred while getting days traffic statistics ordered by total traffic.");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("traffic/ForPeriod/ByDay/export/csv")]
        public async Task<IActionResult> GetDaysTrafficAtPeriodOrderedByDayToCsvAsync([FromQuery] DateTime startPeriod, [FromQuery] DateTime endPeriod, [FromQuery] int maxCount = 0)
        {
            _logger.LogInformation("Getting days traffic statistics at period ordered by day.");

            try
            {
                Expression<Func<DaysStatistics, DateTime>> orderByDay = x => x.Date;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAtPeriodAsync(startPeriod, endPeriod, orderByDay, maxCount);

                var stream = await _csvFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.CsvContentType, $"{TrafficForPeriodByDaysFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics at period ordered by day.");
                return BadRequest("An error occurred while getting days traffic statistics at period ordered by day.");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("traffic/ForPeriod/ByTraffic/export/csv")]
        public async Task<IActionResult> GetDaysTrafficAtPeriodOrderedByTotalTrafficToCsvAsync([FromQuery] DateTime startPeriod, [FromQuery] DateTime endPeriod, [FromQuery] int maxCount = 0)
        {
            _logger.LogInformation("Getting days traffic statistics at period ordered by total traffic.");

            try
            {
                Expression<Func<DaysStatistics, int>> orderByTotalTraffic = x => x.TotalTraffic;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAtPeriodAsync(startPeriod, endPeriod, orderByTotalTraffic, maxCount);

                var stream = await _csvFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.CsvContentType, $"{TrafficForPeriodByTrafficFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics at period ordered by total traffic.");
                return BadRequest("An error occurred while getting days traffic statistics at period ordered by total traffic.");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("traffic/ForHall/ByDay/export/csv")]
        public async Task<IActionResult> GetDaysTrafficAtHallOrderedByDayToCsvAsync([FromQuery] long placeHallId, [FromQuery] int maxCount = 0)
        {
            _logger.LogInformation("Getting days traffic statistics at hall ordered by day.");

            try
            {
                Expression<Func<DaysStatistics, DateTime>> orderByDay = x => x.Date;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAtHallAsync(placeHallId, orderByDay, maxCount);

                var stream = await _csvFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.CsvContentType, $"{TrafficForHallByDaysFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics at hall ordered by day.");
                return BadRequest("An error occurred while getting days traffic statistics at hall ordered by day.");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("traffic/ForHall/ByTraffic/export/csv")]
        public async Task<IActionResult> GetDaysTrafficAtHallOrderedByTotalTrafficToCsvAsync([FromQuery] long placeHallId, [FromQuery] int maxCount = 0)
        {
            _logger.LogInformation("Getting days traffic statistics at hall ordered by total traffic.");

            try
            {
                Expression<Func<DaysStatistics, int>> orderByTotalTraffic = x => x.TotalTraffic;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAtHallAsync(placeHallId, orderByTotalTraffic, maxCount);

                var stream = await _csvFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.CsvContentType, $"{TrafficForHallByTrafficFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics at hall ordered by total traffic.");
                return BadRequest("An error occurred while getting days traffic statistics at hall ordered by total traffic.");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("traffic/ForPlace/ByDay/export/csv")]
        public async Task<IActionResult> GetDaysTrafficAtPlaceOrderedByDayToCsvAsync([FromQuery] long placeAddressId, [FromQuery] int maxCount = 0)
        {
            _logger.LogInformation("Getting days traffic statistics at place ordered by day.");

            try
            {
                Expression<Func<DaysStatistics, DateTime>> orderByDay = x => x.Date;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAtPlaceAsync(placeAddressId, orderByDay, maxCount);

                var stream = await _csvFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.CsvContentType, $"{TrafficForPlaceByDaysFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics at place ordered by day.");
                return BadRequest("An error occurred while getting days traffic statistics at place ordered by day.");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("traffic/ForPlace/ByTraffic/export/csv")]
        public async Task<IActionResult> GetDaysTrafficAtPlaceOrderedByTotalTrafficToCsvAsync([FromQuery] long placeAddressId, [FromQuery] int maxCount = 0)
        {
            _logger.LogInformation("Getting days traffic statistics at place ordered by total traffic.");

            try
            {
                Expression<Func<DaysStatistics, int>> orderByTotalTraffic = x => x.TotalTraffic;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAtPlaceAsync(placeAddressId, orderByTotalTraffic, maxCount);

                var stream = await _csvFileExport.ExportFileAsync(result);
                return File(stream, HelperConstants.CsvContentType, $"{TrafficForPlaceByTrafficFileName}{HelperConstants.CsvExtension}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics at place ordered by total traffic.");
                return BadRequest("An error occurred while getting days traffic statistics at place ordered by total traffic.");
            }
        }
    }
}
