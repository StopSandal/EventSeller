using Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataLayer.Model;
using Microsoft.IdentityModel.Tokens;
using Services.Service;
using EventSeller.DataLayer.EntitiesDto.TicketSeat;
using Microsoft.AspNetCore.Authorization;
using EventSeller.Services.Interfaces.Services;

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
        [Authorize]
        public async Task<IActionResult> GetAsync()
        {
            var list = await _ticketSeatService.GetTicketSeatsAsync();

            if (list.IsNullOrEmpty())
                return NoContent();
            return Ok(list);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAsync(long id)
        {
            var list = await _ticketSeatService.GetByIDAsync(id);
            if (list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpPost]
        [Authorize(Policy = "VenueManagerOrAdmin")]
        public async Task<IActionResult> AddTicketSeatDtoAsync([FromBody] AddTicketSeatDto NewTicketSeat)
        {
            try
            {
                await _ticketSeatService.CreateAsync(NewTicketSeat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while creating the TicketSeat.");
            }
            return Created();
        }
        [HttpPut("{id}")]
        [Authorize(Policy = "VenueManagerOrAdmin")]
        public async Task<IActionResult> EditTicketSeatDtoAsync(long id, [FromBody] EditTicketSeatDto EditTicketSeatDto)
        {
            var existingTicketSeat = await _ticketSeatService.GetByIDAsync(id);

            if (existingTicketSeat == null)
            {
                return NotFound();
            }
            try
            {
                await _ticketSeatService.UpdateAsync(id,EditTicketSeatDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while updating the TicketSeat.");
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        [Authorize(Policy = "VenueManagerOrAdmin")]
        public async Task<IActionResult> DeleteTicketSeatAsync(long id)
        {
            try
            {
                var ticketSeat = await _ticketSeatService.GetByIDAsync(id);
                if (ticketSeat == null)
                    return NotFound();
                await _ticketSeatService.DeleteAsync(id);
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
