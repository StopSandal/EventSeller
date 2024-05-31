using DataLayer.Models.Event;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.Service;
using System.Collections.Generic;

namespace EventSeller.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly IEventService _eventService;

        public EventController(ILogger<EventController> logger, IEventService eventService)
        {
            _logger = logger;
            _eventService = eventService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var list = _eventService.GetEvents();

            if (list.IsNullOrEmpty())
                return NoContent();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var list = _eventService.GetByID(id);
            if (list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpPost]
        public IActionResult AddEventDto([FromBody] AddEventDto NewEvent)
        {
            try
            {
                _eventService.Create(NewEvent);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while creating the event.");
            }
            return Created();
        }
        [HttpPut("{id}")]
        public IActionResult EditEventDto(long id,[FromBody] EditEventDto EditEventDto) 
        {
            var existingEvent = _eventService.GetByID(id);

            if (existingEvent == null)
            {
                return NotFound();
            }
            try
            {
                _eventService.Update(id, EditEventDto);
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while updating the event.");
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(long id) 
        {
            try
            {
                var existingEvent = _eventService.GetByID(id);
                if (existingEvent == null)
                    return NotFound();
                _eventService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while deleting the event.");
            }
        }
    }    
}
