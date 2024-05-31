using Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using DataLayer.Model;
using Services.Service;
using DataLayer.Models.Ticket;

namespace EventSeller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ILogger<TicketController> _logger;
        private readonly ITicketService _ticketService;

        public TicketController(ILogger<TicketController> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var list = _ticketService.GetTickets();

            if (list.IsNullOrEmpty())
                return NoContent();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var list = _ticketService.GetByID(id);
            if (list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpPost]
        public IActionResult AddTicketDto([FromBody] AddTicketDto NewTicket)
        {
            try
            {
                _ticketService.Create(NewTicket);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while creating the Ticket.");
            }
            return Created();
        }
        [HttpPut("{id}")]
        public IActionResult EditTicketDto(long id, [FromBody] EditTicketDto EditTicketDto)
        {
            var existingTicket = _ticketService.GetByID(id);

            if (existingTicket == null)
            {
                return NotFound();
            }
            try
            {
                _ticketService.Update(id,EditTicketDto);
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
                var ticket = _ticketService.GetByID(id);
                if (ticket == null)
                    return NotFound();
                _ticketService.Delete(id);
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
