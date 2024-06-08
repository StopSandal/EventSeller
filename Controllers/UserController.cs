using Microsoft.AspNetCore.Mvc;
using EventSeller.DataLayer.EntitiesViewModel;
using EventSeller.DataLayer.EntitiesDto.User;
using EventSeller.Services.Service;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> GetUser(string userName)
        {
            var user = await _userService.GetUserByUserName(userName);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }

        [HttpPost("CreateUser")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] AddUserDto addUserDto)
        {
            try
            {
                await _userService.CreateUser(addUserDto);
                return Ok("User created successfully");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous] 
        public async Task<IActionResult> Login([FromBody] LoginUserVM user) 
        {
            try
            {
               var token = await _userService.Login(user);

               return Ok(token);
            }
            catch (InvalidDataException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] TokenVM token)
        {
            if (token == null)
            {
                return BadRequest(new { message = "Invalid client request" });
            }

            try
            {
                var newToken = await _userService.RefreshToken(token);
                return Ok(newToken);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("Update/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Update(string id, [FromBody] EditUserDto model)
        {
            try
            {
                await _userService.Update(id, model);
                return Ok("User updated successfully");
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _userService.Delete(id);
                return Ok("User deleted successfully");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
