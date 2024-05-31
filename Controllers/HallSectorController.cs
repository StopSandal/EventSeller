using Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataLayer.Model;
using Microsoft.IdentityModel.Tokens;
using Services.Service;
using DataLayer.Models.HallSector;


namespace hallSectorSeller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HallSectorController : ControllerBase
    {
        private readonly ILogger<HallSectorController> _logger;
        private readonly IHallSectorService _hallSectorService;


        public HallSectorController(ILogger<HallSectorController> logger, IHallSectorService hallSectorService)
        {
            _logger = logger;
            _hallSectorService = hallSectorService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var list = _hallSectorService.GetHallSectors();

            if (list.IsNullOrEmpty())
                return NoContent();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var list = _hallSectorService.GetByID(id);
            if (list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpPost]
        public IActionResult AddHallSectorDto([FromBody] AddHallSectorDto NewHallSector)
        {
            try
            {
                _hallSectorService.Create(NewHallSector);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Sector name should be unique for Hall");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while creating the HallSector.");
            }
            return Created();
        }
        [HttpPut("{id}")]
        public IActionResult EditHallSectorDto(long id, [FromBody] EditHallSectorDto EditHallSectorDto)
        {
            var existingHallSector = _hallSectorService.GetByID(id);

            if (existingHallSector == null)
            {
                return NotFound();
            }
            try
            {
                _hallSectorService.Update(id,EditHallSectorDto);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Sector name should be unique for Hall");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while updating the HallSector.");
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteHallSector(long id)
        {
            try
            {
                var hallSector = _hallSectorService.GetByID(id);
                if (hallSector == null)
                    return NotFound();
                _hallSectorService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while deleting the HallSector.");
            }
        }
    }
}
