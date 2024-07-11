using EventSeller.DataLayer.EntitiesDto.HallSector;
using EventSeller.Services.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EventSeller.Controllers
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
        [Authorize]
        public async Task<IActionResult> GetAsync()
        {
            var list = await _hallSectorService.GetHallSectorsAsync();

            if (list.IsNullOrEmpty())
                return NoContent();
            return Ok(list);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAsync(long id)
        {
            var list = await _hallSectorService.GetByIDAsync(id);
            if (list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpPost]
        [Authorize(Policy = "VenueManagerOrAdmin")]
        public async Task<IActionResult> AddHallSectorDtoAsync([FromBody] AddHallSectorDto NewHallSector)
        {
            try
            {
                await _hallSectorService.CreateAsync(NewHallSector);
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
        [Authorize(Policy = "VenueManagerOrAdmin")]
        public async Task<IActionResult> EditHallSectorDtoAsync(long id, [FromBody] EditHallSectorDto EditHallSectorDto)
        {
            var existingHallSector = await _hallSectorService.GetByIDAsync(id);

            if (existingHallSector == null)
            {
                return NotFound();
            }
            try
            {
                await _hallSectorService.UpdateAsync(id, EditHallSectorDto);
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
        [Authorize(Policy = "VenueManagerOrAdmin")]
        public async Task<IActionResult> DeleteHallSectorAsync(long id)
        {
            try
            {
                var hallSector = await _hallSectorService.GetByIDAsync(id);
                if (hallSector == null)
                    return NotFound();
                await _hallSectorService.DeleteAsync(id);
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
