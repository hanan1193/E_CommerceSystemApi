
using E_CommerceSystemApi.BLL.Services.intf;
using E_CommerceSystemApi.BLL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceSystemApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserCreateViewModel userViewModel)
        {
                var createdUser = await _userService.AddUser(userViewModel);
                return CreatedAtAction(nameof(Get),new {id=createdUser.UserID},createdUser) ;
        }

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
