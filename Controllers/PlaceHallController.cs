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
        public IActionResult Get()
        {
            var list = _placeHallService.GetPlaceHalls();

            if (list.IsNullOrEmpty())
                return NoContent();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var list = _placeHallService.GetByID(id);
            if (list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpPost]
        public IActionResult CreatePlaceHall([FromBody] CreatePlaceHall NewPlaceHall)
        {
            try
            {
                _placeHallService.Create(NewPlaceHall);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while creating the PlaceHall.");
            }
            return Created();
        }
        [HttpPut("{id}")]
        public IActionResult UpdatePlaceHall(long id, [FromBody] UpdatePlaceHall updatePlaceHall)
        {

            var existingPlaceHall = _placeHallService.GetByID(id);

            if (existingPlaceHall == null)
            {
                return NotFound();
            }
            try
            {
                _placeHallService.Update(id, updatePlaceHall);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while updating the PlaceHall.");
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeletePlaceHall(long id)
        {
            try
            {
                var placeHall = _placeHallService.GetByID(id);
                if (placeHall == null)
                    return NotFound();
                _placeHallService.Delete(id);
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
