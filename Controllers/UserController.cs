using Microsoft.AspNetCore.Mvc;
using EventSeller.DataLayer.EntitiesDto.User;
using Microsoft.AspNetCore.Authorization;
using EventSeller.Services.Interfaces.Services;
using EventSeller.DataLayer.EntitiesDto;

namespace EventSeller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetUser/{userName}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetUserAsync(string userName)
        {
            var user = await _userService.GetUserByUserNameAsync(userName);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }

        [HttpPost("CreateUser")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUserAsync([FromBody] AddUserDto addUserDto)
        {
            try
            {
                await _userService.CreateUserAsync(addUserDto);
                return Ok("User created successfully");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous] 
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserDTO user) 
        {
            try
            {
               var token = await _userService.LoginAsync(user);

               return Ok(token);
            }
            catch (InvalidDataException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] TokenDTO token)
        {
            if (token == null)
            {
                return BadRequest(new { message = "Invalid client request" });
            }

            try
            {
                var newToken = await _userService.RefreshTokenAsync(token);
                return Ok(newToken);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("Update/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] EditUserDto model)
        {
            try
            {
                await _userService.UpdateAsync(id, model);
                return Ok("User updated successfully");
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                return Ok("User deleted successfully");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
