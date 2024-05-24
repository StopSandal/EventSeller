using EventSeller.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


namespace EventSeller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HallSectorController : ControllerBase
    {
        private readonly ILogger<HallSectorController> _logger;
        private readonly UnitOfWork _unitOfWork;

        public HallSectorController(ILogger<HallSectorController> logger, UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var list = _unitOfWork.HallSectorRepository.Get();

            if (list.IsNullOrEmpty())
                return NoContent();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var list = _unitOfWork.HallSectorRepository.GetByID(id);
            if (list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpPost]
        public IActionResult CreateHallSector([FromBody] HallSector NewHallSector)
        {
            try
            {
                _unitOfWork.HallSectorRepository.Insert(NewHallSector);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while creating the HallSector.");
            }
            return Created();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateHallSector(long id, [FromBody] HallSector NewHallSector)
        {
            if (id != NewHallSector.ID)
            {
                return BadRequest("ID is mismatch");
            }
            var existingHallSector = _unitOfWork.HallSectorRepository.GetByID(id);

            if (existingHallSector == null)
            {
                return NotFound();
            }
            try
            {
                _unitOfWork.HallSectorRepository.Update(NewHallSector);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while updating the HallSector.");
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteHallSector(long id)
        {
            try
            {
                var list = _unitOfWork.HallSectorRepository.GetByID(id);
                if (list == null)
                    return NotFound();
                _unitOfWork.HallSectorRepository.Delete(id);
                _unitOfWork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while deleting the HallSector.");
            }
        }
    }
}
