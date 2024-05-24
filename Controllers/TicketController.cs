using EventSeller.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EventSeller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ILogger<TicketController> _logger;
        private readonly UnitOfWork _unitOfWork;

        public TicketController(ILogger<TicketController> logger, UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var list = _unitOfWork.TicketRepository.Get();

            if (list.IsNullOrEmpty())
                return NoContent();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var list = _unitOfWork.TicketRepository.GetByID(id);
            if (list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpPost]
        public IActionResult CreateTicket([FromBody] Ticket NewTicket)
        {
            try
            {
                _unitOfWork.TicketRepository.Insert(NewTicket);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while creating the Ticket.");
            }
            return Created();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateTicket(long id, [FromBody] Ticket NewTicket)
        {
            if (id != NewTicket.ID)
            {
                return BadRequest("ID is mismatch");
            }
            var existingTicket = _unitOfWork.TicketRepository.GetByID(id);

            if (existingTicket == null)
            {
                return NotFound();
            }
            try
            {
                _unitOfWork.TicketRepository.Update(NewTicket);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while updating the Ticket.");
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTicket(long id)
        {
            try
            {
                var list = _unitOfWork.TicketRepository.GetByID(id);
                if (list == null)
                    return NotFound();
                _unitOfWork.TicketRepository.Delete(id);
                _unitOfWork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while deleting the Ticket.");
            }
        }
    }
}
