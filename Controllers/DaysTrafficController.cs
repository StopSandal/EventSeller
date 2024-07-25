using EventSeller.DataLayer.EntitiesDto.Statistics;
using EventSeller.Helpers.Constants;
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
        private readonly IResultExportService _resultExportService;
        private readonly ILogger<DaysTrafficController> _logger;

        private const string TrafficByDaysFileName = "TrafficStatisticsByDays";
        private const string TrafficByTrafficFileName = "TrafficStatisticsByDays";
        private const string TrafficForPeriodByDaysFileName = "TrafficStatisticsForPeriodByDays";
        private const string TrafficForPeriodByTrafficFileName = "TrafficStatisticsForPeriodByTraffic";
        private const string TrafficForHallByDaysFileName = "TrafficStatisticsForHallByDays";
        private const string TrafficForHallByTrafficFileName = "TrafficStatisticsForHallByTraffic";
        private const string TrafficForPlaceByDaysFileName = "TrafficStatisticsForPlaceByDays";
        private const string TrafficForPlaceByTrafficFileName = "TrafficStatisticsForPlaceByTraffic";

        public DaysTrafficController(IDayTrafficStatisticService dayTrafficStatisticService, ILogger<DaysTrafficController> logger, IResultExportService resultExportService)
        {
            _dayTrafficStatisticService = dayTrafficStatisticService;
            _logger = logger;
            _resultExportService = resultExportService;
        }

        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("traffic/byDate/export")]
        public async Task<IActionResult> GetDaysTrafficOrderedByDayExportAsync([FromQuery] int maxCount = 0, [FromQuery] string format = "json")
        {
            _logger.LogInformation("Getting days traffic statistics ordered by day.");

            try
            {
                Expression<Func<DaysStatistics, DateTime>> orderByDay = x => x.Date;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAsync(orderByDay, maxCount);

                return await _resultExportService.ExportDataAsync(result, format, TrafficByDaysFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics ordered by day.");
                return BadRequest($"An error occurred while getting days traffic statistics ordered by day. {ex.Message}");
            }
        }

        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("traffic/byTraffic/export")]
        public async Task<IActionResult> GetDaysTrafficOrderedByTotalTrafficExportAsync([FromQuery] int maxCount = 0, [FromQuery] string format = "json")
        {
            _logger.LogInformation("Getting days traffic statistics ordered by total traffic.");

            try
            {
                Expression<Func<DaysStatistics, int>> orderByTotalTraffic = x => x.TotalTraffic;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAsync(orderByTotalTraffic, maxCount);

                return await _resultExportService.ExportDataAsync(result, format, TrafficByTrafficFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics ordered by total traffic.");
                return BadRequest($"An error occurred while getting days traffic statistics ordered by total traffic. {ex.Message}");
            }
        }


        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("traffic/ForPeriod/ByDay/export")]
        public async Task<IActionResult> GetDaysTrafficAtPeriodOrderedByDayExportAsync(
            [FromQuery] DateTime startPeriod,
            [FromQuery] DateTime endPeriod,
            [FromQuery] int maxCount = 0,
            [FromQuery] string format = "json")
        {
            _logger.LogInformation("Getting days traffic statistics at period ordered by day.");

            try
            {
                Expression<Func<DaysStatistics, DateTime>> orderByDay = x => x.Date;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAtPeriodAsync(startPeriod, endPeriod, orderByDay, maxCount);

                return await _resultExportService.ExportDataAsync(result, format, TrafficForPeriodByDaysFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics at period ordered by day.");
                return BadRequest($"An error occurred while getting days traffic statistics at period ordered by day. {ex.Message}");
            }
        }


        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("traffic/ForPeriod/ByTraffic/export")]
        public async Task<IActionResult> GetDaysTrafficAtPeriodOrderedByTotalTrafficExportAsync(
            [FromQuery] DateTime startPeriod,
            [FromQuery] DateTime endPeriod,
            [FromQuery] int maxCount = 0,
            [FromQuery] string format = "json")
        {
            _logger.LogInformation("Getting days traffic statistics at period ordered by total traffic.");

            try
            {
                Expression<Func<DaysStatistics, int>> orderByTotalTraffic = x => x.TotalTraffic;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAtPeriodAsync(startPeriod, endPeriod, orderByTotalTraffic, maxCount);

                return await _resultExportService.ExportDataAsync(result, format, TrafficForPeriodByTrafficFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics at period ordered by total traffic.");
                return BadRequest($"An error occurred while getting days traffic statistics at period ordered by total traffic. {ex.Message}");
            }
        }


        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("traffic/ForHall/ByDay/export")]
        public async Task<IActionResult> GetDaysTrafficAtHallOrderedByDayExportAsync(
            [FromQuery] long placeHallId,
            [FromQuery] int maxCount = 0,
            [FromQuery] string format = "json")
        {
            _logger.LogInformation("Getting days traffic statistics at hall ordered by day.");

            try
            {
                Expression<Func<DaysStatistics, DateTime>> orderByDay = x => x.Date;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAtHallAsync(placeHallId, orderByDay, maxCount);

                return await _resultExportService.ExportDataAsync(result, format, TrafficForHallByDaysFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics at hall ordered by day.");
                return BadRequest($"An error occurred while getting days traffic statistics at hall ordered by day. {ex.Message}");
            }
        }


        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("traffic/ForHall/ByTraffic/export")]
        public async Task<IActionResult> GetDaysTrafficAtHallOrderedByTotalTrafficExportAsync(
            [FromQuery] long placeHallId,
            [FromQuery] int maxCount = 0,
            [FromQuery] string format = "json")
        {
            _logger.LogInformation("Getting days traffic statistics at hall ordered by total traffic.");

            try
            {
                Expression<Func<DaysStatistics, int>> orderByTotalTraffic = x => x.TotalTraffic;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAtHallAsync(placeHallId, orderByTotalTraffic, maxCount);

                return await _resultExportService.ExportDataAsync(result, format, TrafficForHallByTrafficFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics at hall ordered by total traffic.");
                return BadRequest($"An error occurred while getting days traffic statistics at hall ordered by total traffic. {ex.Message}");
            }
        }


        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("traffic/ForPlace/ByDay/export")]
        public async Task<IActionResult> GetDaysTrafficAtPlaceOrderedByDayExportAsync(
            [FromQuery] long placeAddressId,
            [FromQuery] int maxCount = 0,
            [FromQuery] string format = "json")
        {
            _logger.LogInformation($"Getting days traffic statistics at place ordered by day.");

            try
            {
                Expression<Func<DaysStatistics, DateTime>> orderByDay = x => x.Date;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAtPlaceAsync(placeAddressId, orderByDay, maxCount);

                return await _resultExportService.ExportDataAsync(result, format, TrafficForPlaceByDaysFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics at place ordered by day.");
                return BadRequest($"An error occurred while getting days traffic statistics at place ordered by day. {ex.Message}");
            }
        }

        [Authorize(Policy = PoliciesConstants.AdminOnlyPolicy)]
        [HttpGet("traffic/ForPlace/ByTraffic/export")]
        public async Task<IActionResult> GetDaysTrafficAtPlaceOrderedByTotalTrafficExportAsync(
            [FromQuery] long placeAddressId,
            [FromQuery] int maxCount = 0,
            [FromQuery] string format = "json")
        {
            _logger.LogInformation("Getting days traffic statistics at place ordered by total traffic.");

            try
            {
                Expression<Func<DaysStatistics, int>> orderByTotalTraffic = x => x.TotalTraffic;
                var result = await _dayTrafficStatisticService.GetDaysTrafficAtPlaceAsync(placeAddressId, orderByTotalTraffic, maxCount);

                return await _resultExportService.ExportDataAsync(result, format, TrafficForPlaceByTrafficFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting days traffic statistics at place ordered by total traffic.");
                return BadRequest($"An error occurred while getting days traffic statistics at place ordered by total traffic. {ex.Message}");
            }
        }
    }
}
