using EventSeller.DataLayer.EntitiesDto.Statistics;
using EventSeller.Services.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace EventSeller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DaysTrafficController : ControllerBase
    {
        private readonly IDayTrafficStatisticService _dayTrafficStatisticService;
        private readonly ILogger<DaysTrafficController> _logger;

        public DaysTrafficController(IDayTrafficStatisticService dayTrafficStatisticService, ILogger<DaysTrafficController> logger)
        {
            _dayTrafficStatisticService = dayTrafficStatisticService;
            _logger = logger;
        }

        [HttpGet("traffic/byDate")]
        public async Task<IActionResult> GetDaysTrafficOrderedByDay([FromQuery] int maxCount = 0)
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

        [HttpGet("traffic/byTraffic")]
        public async Task<IActionResult> GetDaysTrafficOrderedByTotalTraffic([FromQuery] int maxCount = 0)
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

        [HttpGet("traffic/ForPeriod/ByDay")]
        public async Task<IActionResult> GetDaysTrafficAtPeriodOrderedByDay([FromQuery] DateTime startPeriod, [FromQuery] DateTime endPeriod, [FromQuery] int maxCount = 0)
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

        [HttpGet("traffic/ForPeriod/ByTraffic")]
        public async Task<IActionResult> GetDaysTrafficAtPeriodOrderedByTotalTraffic([FromQuery] DateTime startPeriod, [FromQuery] DateTime endPeriod, [FromQuery] int maxCount = 0)
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

        [HttpGet("traffic/ForHall/ByDay")]
        public async Task<IActionResult> GetDaysTrafficAtHallOrderedByDay([FromQuery] long placeHallId, [FromQuery] int maxCount = 0)
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

        [HttpGet("traffic/ForHall/ByTraffic")]
        public async Task<IActionResult> GetDaysTrafficAtHallOrderedByTotalTraffic([FromQuery] long placeHallId, [FromQuery] int maxCount = 0)
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

        [HttpGet("traffic/ForPlace/ByDay")]
        public async Task<IActionResult> GetDaysTrafficAtPlaceOrderedByDay([FromQuery] long placeAddressId, [FromQuery] int maxCount = 0)
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

        [HttpGet("traffic/ForPlace/ByTraffic")]
        public async Task<IActionResult> GetDaysTrafficAtPlaceOrderedByTotalTraffic([FromQuery] long placeAddressId, [FromQuery] int maxCount = 0)
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
    }
}
