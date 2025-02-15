using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TentBooking.Features.Dtos;
using TentBooking.Models;
using TentBooking.Services;
using AutoMapper;
using SecurityClaim = System.Security.Claims.Claim;
using Claim = System.Security.Claims.Claim;

namespace TentBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TentInventoryDbContext _context;
        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;

        public UserController(TentInventoryDbContext context, TokenService tokenService,IMapper mapper)
        {
            _context = context;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        [Authorize (Roles = "RegisterUserAccess")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginDto user)
        {
            if (_context.Users.Any(u => u.UserEmail == user.UserEmail))
                return BadRequest("User already Exists");
            var newUser = new User
            {
                UserName = user.UserEmail,
                PasswordHash = user.Password,
                UserEmail = user.UserEmail
            };
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return Ok("User registered successfully");
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto login)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserEmail == login.UserEmail && login.Password == u.PasswordHash);
            if (user == null)
                return Unauthorized("Invalid credentials");
            // Usage
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("UserId", user.UserId.ToString())
            };
            var roles = from ur in _context.UserRoles
                        join r in _context.Roles on ur.RoleId equals r.RoleId
                        where ur.UserId == user.UserId
                        select r.RoleName;
            foreach (var role in roles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var claims = from uc in _context.UserClaims
                         join c in _context.Claims on uc.ClaimId equals c.ClaimsId
                         where uc.UserId == user.UserId
                         select new { c.ClaimValue, c.ClaimType };

            foreach (var claim in claims)
            {
                userClaims.Add(new Claim(claim.ClaimType, claim.ClaimValue));
            }
            var token = _tokenService.GenerateToken(user.UserName, userClaims);

            return Ok(new { Token = token });
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUser()
        {
            var users = await _context.Users.ToListAsync();
            if (users == null)
                return NotFound("Data not found");
            return Ok(users);

        }
    }
}
