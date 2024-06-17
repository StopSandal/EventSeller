using EventSeller.Services.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventSeller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRolesService _userRolesService;

        public UserRolesController(IUserRolesService userRolesService)
        {
            _userRolesService = userRolesService;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<string>>> GetAllRoles()
        {
            var roles = await _userRolesService.GetAllRoles();
            return Ok(roles);
        }
        [HttpGet("GetUserRoles/{userName}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<string>>> GetUserRolesByUserName(string userName)
        {
            try
            {
                var roles = await _userRolesService.GetUserRolesByUserName(userName);
                return Ok(roles);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("SetRole/{id}/{role}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> SetRole(string id, string role)
        {
            try
            {
                await _userRolesService.SetRole(id, role);
                return Ok($"Role '{role}' set for user with ID '{id}'");
            }
            catch (InvalidDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("RemoveRole/{id}/{role}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> RemoveRole(string id, string role)
        {
            try
            {
                await _userRolesService.RemoveRole(id, role);
                return Ok($"Role '{role}' removed for user with ID '{id}'");
            }
            catch (InvalidDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
