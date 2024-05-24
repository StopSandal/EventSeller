using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services;

namespace PlaceAddressSeller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceAddressController : ControllerBase
    {
        private readonly ILogger<PlaceAddressController> _logger;
        private readonly UnitOfWork _unitOfWork;

        public PlaceAddressController(ILogger<PlaceAddressController> logger, UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var list = _unitOfWork.PlaceAddressRepository.Get();

            if (list.IsNullOrEmpty())
                return NoContent();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var list = _unitOfWork.PlaceAddressRepository.GetByID(id);
            if (list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpPost]
        public IActionResult CreatePlaceAddress( PlaceAddress NewPlaceAddress)
        {
            try
            {
                _unitOfWork.PlaceAddressRepository.Insert(NewPlaceAddress);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while creating the PlaceAddress.");
            }
            return CreatedAtAction(nameof(Get), new { id = NewPlaceAddress.ID }, NewPlaceAddress); ;
        }
        [HttpPut("{id}")]
        public IActionResult UpdatePlaceAddress(long id, [FromBody] PlaceAddress NewPlaceAddress)
        {
            if (id != NewPlaceAddress.ID)
            {
                return BadRequest("ID is mismatch");
            }
            var existingPlaceAddress = _unitOfWork.PlaceAddressRepository.GetByID(id);

            if (existingPlaceAddress == null)
            {
                return NotFound();
            }
            try
            {
                _unitOfWork.PlaceAddressRepository.Update(NewPlaceAddress);
                _unitOfWork.Save();
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
                var list = _unitOfWork.PlaceAddressRepository.GetByID(id);
                if (list == null)
                    return NotFound();
                _unitOfWork.PlaceAddressRepository.Delete(id);
                _unitOfWork.Save();
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
