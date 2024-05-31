using DataLayer.Model;
using DataLayer.Models.PlaceAddress;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services;
using Services.Service;

namespace PlaceAddressSeller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceAddressController : ControllerBase
    {
        private readonly ILogger<PlaceAddressController> _logger;
        private readonly IPlaceAddressService _placeAddressService;

        public PlaceAddressController(ILogger<PlaceAddressController> logger, IPlaceAddressService placeAddress)
        {
            _logger = logger;
            _placeAddressService = placeAddress;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var list = _placeAddressService.GetPlaceAddresses();

            if (list.IsNullOrEmpty())
                return NoContent();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var list = _placeAddressService.GetByID(id);
            if (list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpPost]
        public IActionResult AddPlaceAddressDto([FromBody] AddPlaceAddressDto NewPlaceAddress)
        {
            try
            {
                _placeAddressService.Create(NewPlaceAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while creating the PlaceAddress.");
            }
            return Created();
        }
        [HttpPut("{id}")]
        public IActionResult EditPlaceAddressDto(long id, [FromBody] EditPlaceAddressDto EditPlaceAddressDto)
        {
            var existingPlaceAddress = _placeAddressService.GetByID(id);

            if (existingPlaceAddress == null)
            {
                return NotFound();
            }
            try
            {
                _placeAddressService.Update(id,EditPlaceAddressDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while updating the PlaceAddress.");
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeletePlaceAddress(long id)
        {
            try
            {
                var placeAddress = _placeAddressService.GetByID(id);
                if (placeAddress == null)
                    return NotFound();
                _placeAddressService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while deleting the PlaceAddress.");
            }
        }
    }
}
