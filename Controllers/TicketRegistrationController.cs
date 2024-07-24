using EventSeller.DataLayer.EntitiesDto.Ticket;
using EventSeller.Helpers.Constants;
using EventSeller.Services.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventSeller.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketRegistrationController : ControllerBase
    {
        private readonly ITicketRegistrationService _ticketRegistrationService;
        private readonly ILogger<TicketRegistrationController> _logger;

        public TicketRegistrationController(
            ITicketRegistrationService ticketRegistrationService,
            ILogger<TicketRegistrationController> logger)
        {
            _ticketRegistrationService = ticketRegistrationService;
            _logger = logger;
        }

        [HttpPost("tickets/add/by-count")]
        [Authorize(Policy = PoliciesConstants.VenueManagerOrAdminPolicy)]
        public async Task<IActionResult> AddTicketsByCount([FromBody] AddTicketsForHallByCountDTO addTicketsForHallByCountDTO)
        {
            try
            {
                _logger.LogInformation("Adding tickets by count for HallID {HallID}", addTicketsForHallByCountDTO.HallID);

                var result = await _ticketRegistrationService.AddTicketsForPlaceHallByCountAsync(addTicketsForHallByCountDTO);

                _logger.LogInformation("{Count} tickets added successfully for HallID {HallID}", result.Count(), addTicketsForHallByCountDTO.HallID);

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Invalid operation error while adding tickets by count for HallID {HallID}", addTicketsForHallByCountDTO.HallID);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding tickets by count for HallID {HallID}", addTicketsForHallByCountDTO.HallID);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("tickets/add/by-seats")]
        [Authorize(Policy = PoliciesConstants.VenueManagerOrAdminPolicy)]
        public async Task<IActionResult> AddTicketsBySeats([FromBody] AddTicketsForHallToFillDTO addTicketsForHallToFillDTO)
        {
            try
            {
                _logger.LogInformation("Adding tickets by seats for HallID {HallID}", addTicketsForHallToFillDTO.HallID);

                var result = await _ticketRegistrationService.AddTicketsForPlaceHallForAllSeatsAsync(addTicketsForHallToFillDTO);

                _logger.LogInformation("{Count} tickets added successfully for HallID {HallID}", result.Count(), addTicketsForHallToFillDTO.HallID);

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Invalid operation error while adding tickets by seats for HallID {HallID}", addTicketsForHallToFillDTO.HallID);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding tickets by seats for HallID {HallID}", addTicketsForHallToFillDTO.HallID);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
