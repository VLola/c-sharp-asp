using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Project_73.Data;
using Project_73.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project_73.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly DbContextClass _context;
        public AuthenticationController(DbContextClass context)
        {
            _context = context;
        }
        [HttpPost("login")]
        public IActionResult Login([FromForm] Login user)
        {
            if (!TryValidateModel(user, nameof(Login)))
                return BadRequest();
            ModelState.ClearValidationState(nameof(Login));
            if (user is null)
            {
                return BadRequest("Invalid user request!!!");
            }
            if (_context.Logins.Any(item=>item.UserName == user.UserName && item.Password == user.Password))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSetting["JWT:Secret"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: ConfigurationManager.AppSetting["JWT:ValidIssuer"],
                    audience: ConfigurationManager.AppSetting["JWT:ValidAudience"],
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(6),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new JWTTokenResponse { Token = tokenString });
            }
            return Unauthorized();
        }

        [HttpGet("{id}")]
        public IActionResult GetValue(int id)
        {
            var value = _context.Logins.Find(id);
            return Ok(value);
        }

        [HttpPost("registration")]
        public IActionResult Add([FromForm] Login user)
        {
            if (!TryValidateModel(user, nameof(Login)))
                return BadRequest();
            ModelState.ClearValidationState(nameof(Login));
            if (_context.Logins.Any(item=>item.UserName == user.UserName))
            {
                return Conflict();
            }
            else
            {
                _context.Logins.Add(user);
                _context.SaveChanges();
                string uri = $"/api/registration/{user.Id}";
                return Created(uri, user);
            }
        }
    }
}
