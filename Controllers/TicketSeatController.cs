using EventSeller.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EventSeller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketSeatController : ControllerBase
    {
        private readonly ILogger<TicketSeatController> _logger;
        private readonly UnitOfWork _unitOfWork;

        public TicketSeatController(ILogger<TicketSeatController> logger, UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var list = _unitOfWork.TicketSeatRepository.Get();

            if (list.IsNullOrEmpty())
                return NoContent();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var list = _unitOfWork.TicketSeatRepository.GetByID(id);
            if (list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpPost]
        public IActionResult CreateTicketSeat([FromBody] TicketSeat NewTicketSeat)
        {
            try
            {
                _unitOfWork.TicketSeatRepository.Insert(NewTicketSeat);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while creating the TicketSeat.");
            }
            return Created();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateTicketSeat(Guid id, [FromBody] TicketSeat NewTicketSeat)
        {
            if (id != NewTicketSeat.ID)
            {
                return BadRequest("ID is mismatch");
            }
            var existingTicketSeat = _unitOfWork.TicketSeatRepository.GetByID(id);

            if (existingTicketSeat == null)
            {
                return NotFound();
            }
            try
            {
                _unitOfWork.TicketSeatRepository.Update(NewTicketSeat);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while updating the TicketSeat.");
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTicketSeat(long id)
        {
            try
            {
                _unitOfWork.TicketSeatRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while deleting the TicketSeat.");
            }
        }
    }
}
