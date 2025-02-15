using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TentBooking.Models;

namespace TentBooking.Controllers
{
    [Authorize(Policy = "ClaimAccess")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private readonly TentInventoryDbContext _context;
        public ClaimController(TentInventoryDbContext context)
        {
            _context = context;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateClaim(string claimType,string claimValue)
        {
            var claim = new Claim { ClaimType = claimType, ClaimValue = claimValue };
            await _context.Claims.AddAsync(claim);
            await _context.SaveChangesAsync();
            return Ok("Claim created successfully");
        }
        [HttpPost("assign")]
        public async Task<IActionResult> AssignClaimToUser(int userId,int claimId)
        {
            await _context.UserClaims.AddAsync(new UserClaim { UserId = userId, ClaimId = claimId });
            await _context.SaveChangesAsync();
            return Ok("Claim assign successfully");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateClaim(int claimId,string claimValue,string claimType)
        {
           var result = await _context.Claims.FindAsync(claimId);
            if (result == null)
                return NotFound("Not Found ClaimId");
            result.ClaimValue = claimValue;
            result.ClaimType = claimType;
            _context.Update(result);
            await _context.SaveChangesAsync();
            return Ok("Update claim successfully");
        }
        [HttpGet("claims")]
        public async Task<IActionResult> GetClaims()
        {
            var claims = await _context.Claims.ToListAsync();
            return Ok(claims);

        }

        [HttpGet("userClaims")]
        public async Task<IActionResult> GetUserClaim()
        {
            var userClaims = await _context.UserClaims.ToListAsync();
            return Ok(userClaims);

        }
    }
}
