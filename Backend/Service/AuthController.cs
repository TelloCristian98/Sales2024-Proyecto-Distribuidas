using Entities;
using Microsoft.AspNetCore.Mvc;
using DAL;
using Security;

namespace Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SalesDbContext _context;
        private readonly JWTService _jwtService;

        public AuthController(SalesDbContext context, JWTService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);

            if (user == null || !PasswordHasher.VerifyPassword(request.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = _jwtService.GenerateToken(user.UserId, user.Username, user.Role);
            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            if (_context.Users.Any(u => u.Username == request.Username || u.Email == request.Email))
            {
                return BadRequest("Username or email already exists.");
            }

            var hashedPassword = PasswordHasher.HashPassword(request.Password);
            var user = new Users
            {
                Username = request.Username,
                PasswordHash = hashedPassword,
                Email = request.Email,
                Role = "Viewer" // Default role
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User registered successfully.");
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}