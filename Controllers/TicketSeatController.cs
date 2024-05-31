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
        public async Task<IActionResult> GetAsync()
        {
            var list = await _ticketSeatService.GetTicketSeats();

            if (list.IsNullOrEmpty())
                return NoContent();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            var list = await _ticketSeatService.GetByID(id);
            if (list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpPost]
        public async Task<IActionResult> AddTicketSeatDtoAsync([FromBody] AddTicketSeatDto NewTicketSeat)
        {
            try
            {
                await _ticketSeatService.Create(NewTicketSeat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while creating the TicketSeat.");
            }
            return Created();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditTicketSeatDtoAsync(long id, [FromBody] EditTicketSeatDto EditTicketSeatDto)
        {
            var existingTicketSeat = await _ticketSeatService.GetByID(id);

            if (existingTicketSeat == null)
            {
                return NotFound();
            }
            try
            {
                await _ticketSeatService.Update(id,EditTicketSeatDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while updating the TicketSeat.");
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketSeatAsync(long id)
        {
            try
            {
                var ticketSeat = await _ticketSeatService.GetByID(id);
                if (ticketSeat == null)
                    return NotFound();
                await _ticketSeatService.Delete(id);
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
