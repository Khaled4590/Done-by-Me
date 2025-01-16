using Microsoft.AspNetCore.Mvc;
using CryptoExchangeAPI.Models;

namespace CryptoExchangeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            _userService.CreateUser(user);
            return Ok(new { message = "User registered successfully!" });
        }

        [HttpGet("get-user")]
        public IActionResult GetUser(string email)
        {
            var user = _userService.GetUserByEmail(email);
            if (user == null) return NotFound();

            return Ok(user);
        }
    }
}
