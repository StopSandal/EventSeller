using EventSeller.DataLayer.EntitiesDto.PlaceHall;
using EventSeller.Helpers.Constants;
using EventSeller.Services.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EventSeller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceHallController : ControllerBase
    {
        private readonly ILogger<PlaceHallController> _logger;
        private readonly IPlaceHallService _placeHallService;

        public PlaceHallController(ILogger<PlaceHallController> logger, IPlaceHallService placeHallService)
        {
            _logger = logger;
            _placeHallService = placeHallService;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAsync()
        {
            var list = await _placeHallService.GetPlaceHallsAsync();

            if (list.IsNullOrEmpty())
                return NoContent();
            return Ok(list);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAsync(long id)
        {
            var list = await _placeHallService.GetByIDAsync(id);
            if (list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpPost]
        [Authorize(Policy = PoliciesConstants.VenueManagerOrAdminPolicy)]
        public async Task<IActionResult> AddPlaceHallAsync([FromBody] AddPlaceHallDto NewPlaceHall)
        {
            try
            {
                await _placeHallService.CreateAsync(NewPlaceHall);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Hall name should be unique for Place");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while creating the PlaceHall.");
            }
            return Created();
        }
        [HttpPut("{id}")]
        [Authorize(Policy = PoliciesConstants.VenueManagerOrAdminPolicy)]
        public async Task<IActionResult> EditPlaceHallAsync(long id, [FromBody] EditPlaceHallDto EditPlaceHallDto)
        {

            var existingPlaceHall = await _placeHallService.GetByIDAsync(id);

            if (existingPlaceHall == null)
            {
                return NotFound();
            }
            try
            {
                await _placeHallService.UpdateAsync(id, EditPlaceHallDto);

            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Hall name should be unique for Place");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while updating the PlaceHall.");
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        [Authorize(Policy = PoliciesConstants.VenueManagerOrAdminPolicy)]
        public async Task<IActionResult> DeletePlaceHallAsync(long id)
        {
            try
            {
                var placeHall = await _placeHallService.GetByIDAsync(id);
                if (placeHall == null)
                    return NotFound();
                await _placeHallService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while deleting the PlaceHall.");
            }
        }
    }
}
