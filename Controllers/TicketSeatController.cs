using Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataLayer.Model;
using Microsoft.IdentityModel.Tokens;
using Services.Service;
using DataLayer.Models.TicketSeat;

namespace EventSeller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketSeatController : ControllerBase
    {
        private readonly ILogger<TicketSeatController> _logger;
        private readonly ITicketSeatService _ticketSeatService;

        public TicketSeatController(ILogger<TicketSeatController> logger, ITicketSeatService ticketSeatService)
        {
            _logger = logger;
            _ticketSeatService = ticketSeatService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var list = _ticketSeatService.GetTicketSeats();

            if (list.IsNullOrEmpty())
                return NoContent();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var list = _ticketSeatService.GetByID(id);
            if (list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpPost]
        public IActionResult CreateTicketSeat([FromBody] CreateTicketSeat NewTicketSeat)
        {
            try
            {
                _ticketSeatService.Create(NewTicketSeat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while creating the TicketSeat.");
            }
            return Created();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateTicketSeat(long id, [FromBody] UpdateTicketSeat updateTicketSeat)
        {
            var existingTicketSeat = _ticketSeatService.GetByID(id);

            if (existingTicketSeat == null)
            {
                return NotFound();
            }
            try
            {
                _ticketSeatService.Update(id,updateTicketSeat);
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
                var ticketSeat = _ticketSeatService.GetByID(id);
                if (ticketSeat == null)
                    return NotFound();
                _ticketSeatService.Delete(id);
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
