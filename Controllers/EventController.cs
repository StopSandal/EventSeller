
using DataLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services;
using System.Collections.Generic;

namespace EventSeller.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly UnitOfWork _unitOfWork;

        public EventController(ILogger<EventController> logger, UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var list = _unitOfWork.EventRepository.Get();

            if (list.IsNullOrEmpty())
                return NoContent();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var list = _unitOfWork.EventRepository.GetByID(id);
            if (list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpPost]
        public IActionResult CreateEvent([FromBody] Event NewEvent)
        {
            try
            {
                _unitOfWork.EventRepository.Insert(NewEvent);
                _unitOfWork.Save();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while creating the event.");
            }
            return Created();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateEvent(long id,[FromBody] Event NewEvent) 
        {
            if (id != NewEvent.ID)
            {
                return BadRequest("ID is mismatch");
            }
            var existingEvent = _unitOfWork.EventRepository.GetByID(id);

            if (existingEvent == null)
            {
                return NotFound();
            }
            try
            {
                _unitOfWork.EventRepository.Update(NewEvent);
                _unitOfWork.Save();
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
                var list = _unitOfWork.EventRepository.GetByID(id);
                if (list == null)
                    return NotFound();
                _unitOfWork.EventRepository.Delete(id);
                _unitOfWork.Save();
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
