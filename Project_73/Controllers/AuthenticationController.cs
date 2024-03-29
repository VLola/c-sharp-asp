﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult> Login([FromForm] Login user)
        {
            if (!TryValidateModel(user, nameof(Login)))
                return BadRequest();
            ModelState.ClearValidationState(nameof(Login));
            if (user is null)
            {
                return BadRequest("Invalid user request!!!");
            }
            if (await _context.Logins.AnyAsync(item=>item.Email == user.Email && item.Password == user.Password))
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
        public async Task<ActionResult> GetValue(int id)
        {
            var value = await _context.Logins.FindAsync(id);
            return Ok(value);
        }

        [HttpPost("registration")]
        public async Task<ActionResult> Add([FromForm] Login user)
        {
            if (!TryValidateModel(user, nameof(Login)))
                return BadRequest();
            ModelState.ClearValidationState(nameof(Login));
            var addr = new System.Net.Mail.MailAddress(user.Email);
            if (addr.Address == user.Email)
            {
                if (await _context.Logins.AnyAsync(item => item.Email == user.Email))
                {
                    return Conflict();
                }
                else
                {
                    await _context.Logins.AddAsync(user);
                    await _context.SaveChangesAsync();
                    string uri = $"/api/registration/{user.Id}";
                    return Created(uri, user);
                }
            }
            else return BadRequest();
        }
    }
}
