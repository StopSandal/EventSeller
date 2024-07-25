using EventSeller.DataLayer.EntitiesDto.Ticket;
using EventSeller.Helpers.Constants;
using EventSeller.Services.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
        [Authorize]
        public async Task<IActionResult> GetAsync()
        {
            var list = await _ticketService.GetTicketsAsync();

            if (list.IsNullOrEmpty())
                return NoContent();
            return Ok(list);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAsync(long id)
        {
            var list = await _ticketService.GetByIDAsync(id);
            if (list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpPost]
        [Authorize(Policy = PoliciesConstants.VenueManagerOrAdminPolicy)]
        public async Task<IActionResult> AddTicketAsync([FromBody] AddTicketDto NewTicket)
        {
            try
            {
                await _ticketService.CreateAsync(NewTicket);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while creating the Ticket.");
            }
            return Created();
        }
        [HttpPut("{id}")]
        [Authorize(Policy = PoliciesConstants.VenueManagerOrAdminPolicy)]
        public async Task<IActionResult> EditTicketAsync(long id, [FromBody] EditTicketDto EditTicketDto)
        {
            var existingTicket = await _ticketService.GetByIDAsync(id);

            if (existingTicket == null)
            {
                return NotFound();
            }
            try
            {
                await _ticketService.UpdateAsync(id, EditTicketDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while updating the Ticket.");
            }

            return NoContent();
        }
        [Authorize(Policy = PoliciesConstants.VenueManagerOrAdminPolicy)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketAsync(long id)
        {
            try
            {
                var ticket = await _ticketService.GetByIDAsync(id);
                if (ticket == null)
                    return NotFound();
                await _ticketService.DeleteAsync(id);
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
