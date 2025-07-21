using E_CommerceSystemApi.BLL.Services.impl;
using E_CommerceSystemApi.BLL.Services.intf;
using E_CommerceSystemApi.BLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceSystemApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [AllowAnonymous]
        //api/roles/
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleService.GetRoles();
            if (roles == null || !roles.Any())
            {
                return NotFound("No roles found.");
            }
            return Ok(roles);
        }
        [Authorize(Roles = "Admin")]
        // api/roles/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            var role = await _roleService.GetRoleById(id);
            if (role == null)
            {
                return NotFound("$Role with ID {id} not found.");
            }
            return Ok(role);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleViewModel role)
        {
            if(role==null)
            {
                return BadRequest("Role data is required.");
            }
           var createdRole= await _roleService.AddRole(role);
            return CreatedAtAction(nameof(GetRole), new { id = createdRole.RoleID }, createdRole);

        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateRole([FromBody] RoleViewModel role)
        {
            try
            {
                await _roleService.UpdateRole(role);
                return Ok(role);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteRole(int id)
        //{
        //    var role = await _roleService.GetRoleById(id);
        //    if(role ==null)
        //    {
        //        return NotFound("$ Role with ID {id} not found.");
        //    }
        //    await _roleService.DeleteRole(id);
        //    return Ok("$ Role with ID {id} deleted successfully.");
        //}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
    
            var role = await _roleService.GetRoleById(id);
            if (role == null)
            {
                return NotFound($"Role with ID {id} not found.");
            }

            try
            {
                var deleted = await _roleService.DeleteRole(id);

                if (!deleted)               
                {
                    return StatusCode(500, $"Role with ID {id} could not be deleted.");
                }
                return Ok($"Role with ID {id} deleted successfully.");
            }
            catch (DbUpdateException ex)
            {
                return Conflict($"Cannot delete role with ID {id}. " +
                                "Make sure no entities reference this role. " +
                                $"Details: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

    }
}
