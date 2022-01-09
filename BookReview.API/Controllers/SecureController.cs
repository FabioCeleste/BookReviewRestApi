using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using BookReview.Application.Dtos;
using BookReview.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BCryptNet = BCrypt.Net.BCrypt;

namespace BookReview.API.Controllers
{
    public class SecureController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;

        public SecureController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        private async Task<bool> ValidateUser(LoginDto login)
        {
            var user= await _userService.GetAllUsersByEmailAsync(login.Email);

            if(user == null) {
                return false;
            }

            if (BCryptNet.Verify(login.Password, user.Password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string TokenBuild()
        {
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(120);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: expiry,
                signingCredentials: credentials
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginDto login)
        {
            bool resultado = await ValidateUser(login);
            if (resultado)
            {
                var tokenString = TokenBuild();
                return Ok(new TokenDto { Token = tokenString, DateToken = DateTime.Now});
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}