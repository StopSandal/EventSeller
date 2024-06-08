using DataLayer.Model;
using DataLayer.Models.PlaceAddress;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult> GetAsync()
        {
            var list = await _placeAddressService.GetPlaceAddresses();

            if (list.IsNullOrEmpty())
                return NoContent();
            return Ok(list);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAsync(long id)
        {
            var list = await _placeAddressService.GetByID(id);
            if (list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpPost]
        [Authorize(Policy = "VenueManagerOrAdmin")]
        public async Task<IActionResult> AddPlaceAddressDtoAsync([FromBody] AddPlaceAddressDto NewPlaceAddress)
        {
            try
            {
                await _placeAddressService.Create(NewPlaceAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while creating the PlaceAddress.");
            }
            return Created();
        }
        [HttpPut("{id}")]
        [Authorize(Policy = "VenueManagerOrAdmin")]
        public async Task<IActionResult> EditPlaceAddressDtoAsync(long id, [FromBody] EditPlaceAddressDto EditPlaceAddressDto)
        {
            var existingPlaceAddress = await _placeAddressService.GetByID(id);

            if (existingPlaceAddress == null)
            {
                return NotFound();
            }
            try
            {
                await _placeAddressService.Update(id,EditPlaceAddressDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while updating the PlaceAddress.");
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        [Authorize(Policy = "VenueManagerOrAdmin")]
        public async Task<IActionResult> DeletePlaceAddressAsync(long id)
        {
            try
            {
                var placeAddress = await _placeAddressService.GetByID(id);
                if (placeAddress == null)
                    return NotFound();
                await _placeAddressService.Delete(id);
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
