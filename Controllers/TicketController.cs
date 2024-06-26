﻿using Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using DataLayer.Model;
using Services.Service;
using DataLayer.Models.Ticket;
using Microsoft.AspNetCore.Authorization;

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
            var list = await _ticketService.GetTickets();

            if (list.IsNullOrEmpty())
                return NoContent();
            return Ok(list);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAsync(long id)
        {
            var list = await _ticketService.GetByID(id);
            if (list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpPost]
        [Authorize(Policy = "VenueManagerOrAdmin")]
        public async Task<IActionResult> AddTicketDtoAsync([FromBody] AddTicketDto NewTicket)
        {
            try
            {
                await _ticketService.Create(NewTicket);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while creating the Ticket.");
            }
            return Created();
        }
        [HttpPut("{id}")]
        [Authorize(Policy = "VenueManagerOrAdmin")]
        public async Task<IActionResult> EditTicketDtoAsync(long id, [FromBody] EditTicketDto EditTicketDto)
        {
            var existingTicket = await _ticketService.GetByID(id);

            if (existingTicket == null)
            {
                return NotFound();
            }
            try
            {
                await _ticketService.Update(id,EditTicketDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while updating the Ticket.");
            }

            return NoContent();
        }
        [Authorize(Policy = "VenueManagerOrAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketAsync(long id)
        {
            try
            {
                var ticket = await _ticketService.GetByID(id);
                if (ticket == null)
                    return NotFound();
                await _ticketService.Delete(id);
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
