using Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataLayer.Model;
using Microsoft.IdentityModel.Tokens;
using Services.Service;
using DataLayer.Models.PlaceHall;

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
        public async Task<IActionResult> GetAsync()
        {
            var list = await _placeHallService.GetPlaceHalls();

            if (list.IsNullOrEmpty())
                return NoContent();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            var list = await _placeHallService.GetByID(id);
            if (list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpPost]
        public async Task<IActionResult> AddPlaceHallDtoAsync([FromBody] AddPlaceHallDto NewPlaceHall)
        {
            try
            {
                await _placeHallService.Create(NewPlaceHall);
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
        public async Task<IActionResult> EditPlaceHallDtoAsync(long id, [FromBody] EditPlaceHallDto EditPlaceHallDto)
        {

            var existingPlaceHall = await _placeHallService.GetByID(id);

            if (existingPlaceHall == null)
            {
                return NotFound();
            }
            try
            {
                await _placeHallService.Update(id, EditPlaceHallDto);

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
        public async Task<IActionResult> DeletePlaceHallAsync(long id)
        {
            try
            {
                var placeHall = await _placeHallService.GetByID(id);
                if (placeHall == null)
                    return NotFound();
                await _placeHallService.Delete(id);
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
