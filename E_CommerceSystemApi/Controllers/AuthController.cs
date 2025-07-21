using E_CommerceSystemApi.BLL.Services.intf;
using E_CommerceSystemApi.BLL.ViewModels;
using E_CommerceSystemApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _jwt;
        private readonly IUserService _userService;

        public AuthController(JwtTokenService jwt, IUserService userService)
        {
            _jwt = jwt;
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto login)
        {
            var user = _userService.Authenticate(login.Email, login.Password);
            if (user == null)
                return Unauthorized("Invalid credentials");

            var token = _jwt.GenerateToken(user.UserID.ToString(), user.role.RoleName);

            return Ok(new
            {
                Token = token,
                UserId = user.UserID,
                Name = user.Name,
                Role = user.role.RoleName
            });
        }
    }
}
