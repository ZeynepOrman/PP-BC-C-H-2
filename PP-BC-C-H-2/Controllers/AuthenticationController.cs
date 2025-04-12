using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace PP_BC_C_H_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromForm] string username, [FromForm] string password)
        {
            var configUsername = _configuration["UserCredentials:Username"];
            var configPassword = _configuration["UserCredentials:Password"];

            if (username == configUsername && password == configPassword)
            {
                HttpContext.Session.SetInt32("authenticationcompleted", 1);
                return Ok(new { status = 200, message = "Login successful" });
            }

            return Unauthorized(new { status = 401, error = "Invalid username or password" });
        }
    }
}
