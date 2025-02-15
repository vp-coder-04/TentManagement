using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TentBooking.Models;

namespace TentBooking.Controllers
{
    [Authorize(Policy = "RoleAccess")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly TentInventoryDbContext _context;
        public RoleController(TentInventoryDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (_context.Roles.Any(r => r.RoleName == roleName))
                return BadRequest("Role already exists");
            var role = new Role {  RoleName = roleName };
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return Ok("Role added successfully");
        }

        [HttpPost("admin")]
        public async Task<IActionResult> AssignRoleToUser(int userId,int roleId)
        {
            if(!_context.Users.Any(u=>u.UserId==userId)||!_context.Roles.Any(r=>r.RoleId==roleId))
                return NotFound("User or Role not found");
            await _context.UserRoles.AddAsync(new UserRole { UserId = userId, RoleId = roleId });
            await _context.SaveChangesAsync();
            return Ok("Role assign successfully");
        }
        
        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            return Ok(roles);

        }

        [HttpGet("userRoles")]
        public async Task<IActionResult> GetUserRoles()
        {
            var userRoles = await _context.UserRoles.ToListAsync();
            return Ok(userRoles);
        }
    }
}
