using EventSeller.DataLayer.EntitiesDto.Event;
using EventSeller.Helpers.Constants;
using EventSeller.Services.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


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
        [Authorize]
        public async Task<IActionResult> GetAsync()
        {
            var list = await _eventService.GetEventsAsync();

            if (list.IsNullOrEmpty())
                return NoContent();
            return Ok(list);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAsync(long id)
        {
            var list = await _eventService.GetByIDAsync(id);
            if (list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpPost]
        [Authorize(Policy = PoliciesConstants.EventManagerOrAdminPolicy)]
        public async Task<IActionResult> AddEventDtoAsync([FromBody] AddEventDto NewEvent)
        {
            try
            {
                await _eventService.CreateAsync(NewEvent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while creating the event.");
            }
            return Created();
        }
        [HttpPut("{id}")]
        [Authorize(Policy = PoliciesConstants.EventManagerOrAdminPolicy)]
        public async Task<IActionResult> EditEventDtoAsync(long id, [FromBody] EditEventDto EditEventDto)
        {
            var existingEvent = await _eventService.GetByIDAsync(id);

            if (existingEvent == null)
            {
                return NotFound();
            }
            try
            {
                await _eventService.UpdateAsync(id, EditEventDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while updating the event.");
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        [Authorize(Policy = PoliciesConstants.EventManagerOrAdminPolicy)]
        public async Task<IActionResult> DeleteEventAsync(long id)
        {
            try
            {
                var existingEvent = await _eventService.GetByIDAsync(id);
                if (existingEvent == null)
                    return NotFound();
                await _eventService.DeleteAsync(id);
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
