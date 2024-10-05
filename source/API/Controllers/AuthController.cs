using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MedicalProject.Application.DTOs;
using MedicalProject.Application.Services;
using MedicalProject.Core.Ports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MedicalProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto model)
        {
            try
            {
                var result = await _userService.RegisterUserAsync(model);

                if (result)
                {
                    return Ok(new { Message = "User registered successfully" });
                }
                else
                {
                    return BadRequest(new { Message = "User registration failed. User may already exist or there was an error." });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again.");
            }
        }

        [HttpPost("verify")]
        public async Task<IActionResult> Verify([FromBody] VerifyUserDto model)
        {
            try
            {
                var (isValid, isError) = await _userService.VerifyUserAsync(model);

                // If there is no error and user is valid
                if (!isError && isValid)
                {
                    var token = GenerateJwtToken(model.Username);
                    return Ok(new { Message = "Verification successful", Token = token });
                }
                else if (isError)
                {
                    // Return generic error message
                    return StatusCode(500, "An unexpected error occurred. Please try again.");
                }
                else
                {
                    return Unauthorized("Invalid credentials.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An unexpected error occurred. Please try again. {e}");
            }
        }

        private string GenerateJwtToken(string username)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings.GetValue<string>("SecretKey");
            var issuer = jwtSettings.GetValue<string>("Issuer");
            var audience = jwtSettings.GetValue<string>("Audience");
            var expiryMinutes = jwtSettings.GetValue<int>("ExpiryMinutes");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Добавляем утверждения (claims)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                // Можно добавить дополнительные claims, например, роли пользователя
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}