using EventSeller.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


namespace EventSeller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceHallController : ControllerBase
    {
        private readonly ILogger<PlaceHallController> _logger;
        private readonly UnitOfWork _unitOfWork;

        public PlaceHallController(ILogger<PlaceHallController> logger, UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var list = _unitOfWork.PlaceHallRepository.Get();

            if (list.IsNullOrEmpty())
                return NoContent();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var list = _unitOfWork.PlaceHallRepository.GetByID(id);
            if (list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpPost]
        public IActionResult CreatePlaceHall([FromBody] PlaceHall NewPlaceHall)
        {
            try
            {
                _unitOfWork.PlaceHallRepository.Insert(NewPlaceHall);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while creating the PlaceHall.");
            }
            return Created();
        }
        [HttpPut("{id}")]
        public IActionResult UpdatePlaceHall(long id, [FromBody] PlaceHall NewPlaceHall)
        {
            if (id != NewPlaceHall.ID)
            {
                return BadRequest("ID is mismatch");
            }
            var existingPlaceHall = _unitOfWork.PlaceHallRepository.GetByID(id);

            if (existingPlaceHall == null)
            {
                return NotFound();
            }
            try
            {
                _unitOfWork.PlaceHallRepository.Update(NewPlaceHall);
                _unitOfWork.Save();
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
                _unitOfWork.PlaceHallRepository.Delete(id);
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
