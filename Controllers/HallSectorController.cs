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
        public async Task<IActionResult> GetAsync()
        {
            var list = await _hallSectorService.GetHallSectors();

            if (list.IsNullOrEmpty())
                return NoContent();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            var list = await _hallSectorService.GetByID(id);
            if (list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpPost]
        public async Task<IActionResult> AddHallSectorDtoAsync([FromBody] AddHallSectorDto NewHallSector)
        {
            try
            {
                await _hallSectorService.Create(NewHallSector);
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
        public async Task<IActionResult> EditHallSectorDtoAsync(long id, [FromBody] EditHallSectorDto EditHallSectorDto)
        {
            var existingHallSector = await _hallSectorService.GetByID(id);

            if (existingHallSector == null)
            {
                return NotFound();
            }
            try
            {
                await _hallSectorService.Update(id,EditHallSectorDto);
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
        public async Task<IActionResult> DeleteHallSectorAsync(long id)
        {
            try
            {
                var hallSector = await _hallSectorService.GetByID(id);
                if (hallSector == null)
                    return NotFound();
                await _hallSectorService.Delete(id);
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
