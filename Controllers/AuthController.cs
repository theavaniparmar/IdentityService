using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using IdentityService.Models;
using System.Security.Claims;
using System.Text;


namespace IdentityService.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]

        public IActionResult Login([FromBody] Models.LoginRequest request)
        {
            if (request.Username == "Admin" && request.Password == "pwd")
            {
                var token = GenerateJwtToken(request.Username, "Admin");
                return Ok(new { token });
            }
            if (request.Username == "User" && request.Password == "PWD")
            {
                var token = GenerateJwtToken(request.Username, "User");
                return Ok(new { token });
            }
            return Unauthorized();
        }

        private string GenerateJwtToken(String Username, string role)
        {
            var jwtset = _configuration.GetSection("JwtSettings");
            var secretkey = Encoding.UTF8.GetBytes(jwtset["Secret"]);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(secretkey);
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtset["Issuer"],
                audience: jwtset["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtset["ExpiryMinutes"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}



        