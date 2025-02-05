using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public string Authenticate([FromBody] string Name,string Password)
        {
            var token = _authService.Authenticate(Name,Password);
            return token;
        }

    }
}