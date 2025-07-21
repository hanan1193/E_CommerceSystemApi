
using E_CommerceSystemApi.BLL.Services.intf;
using E_CommerceSystemApi.BLL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace E_CommerceSystemApi.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        // Publicly accessible endpoint (No authentication required)
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }
        // Authenticated endpoint (Requires valid JWT token)
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }
        // Endpoint restricted to Admin role
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(UserCreateViewModel userViewModel)
        {
                var createdUser = await _userService.AddUser(userViewModel);
                return CreatedAtAction(nameof(Get),new {id=createdUser.UserID},createdUser) ;
        }
        // Endpoint restricted to Admin role
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserUpdateViewModel user)
        {
            try
            {
                await _userService.UpdateUser(user);
                    return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        // Endpoint restricted to Admin role
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteUser(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return NotFound(new { message = ex.Message });

            }
            
        }
    }
}
