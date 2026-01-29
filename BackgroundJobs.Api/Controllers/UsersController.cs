using BackgroundJobs.Api.Models;
using BackgroundJobs.Api.Services;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackgroundJobs.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IEmailService _emailService;   

        public UsersController(IUserService userService, IBackgroundJobClient backgroundJobClient, IEmailService emailService)
        {
            _userService = userService;
            _backgroundJobClient = backgroundJobClient;
            _emailService = emailService;
        }

        [HttpPost("user")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDto)
        {
            var user = await _userService.CreateAsync(userDto.Name, userDto.Email);
            _backgroundJobClient.Enqueue(() => _emailService.SendWelcomeEmailAsync(user.Email, user.Name));
            return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, user);
        }

        [HttpPost("with-error")]
        public async Task<IActionResult> CreateWithEmailError([FromBody] UserDTO userDto)
        {
            var user = await _userService.CreateAsync(userDto.Name, userDto.Email);
            _backgroundJobClient.Enqueue(() => _emailService.SendWelcomeEmailWithErrorAsync(user.Email, user.Name));
            return CreatedAtAction(nameof(CreateWithEmailError), new { id = user.Id }, user);
        }
    }
}
