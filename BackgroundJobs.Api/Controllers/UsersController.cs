using BackgroundJobs.Api.Models;
using BackgroundJobs.Api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackgroundJobs.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("user")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDto)
        {
            var user = await _userService.CreateAsync(userDto.Name, userDto.Email);
            return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, user);
        }
    }
}
