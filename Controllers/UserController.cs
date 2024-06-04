using EventSeller.DataLayer.Entities;
using EventSeller.DataLayer.EntitiesViewModel;
using EventSeller.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventSeller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginUserVM user)
        {
            var token = await userService.Login(user);
            if (token == null || token==string.Empty) {
                return BadRequest(new { message = "UserName or Password is incorrect" });
            }
            return Ok(token);
        }

    }
}
