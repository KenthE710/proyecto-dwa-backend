using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using App.Services.AuthService;
using App.Dto.Auth;
using App.Models;

namespace App.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService auth, IConfiguration configuration)
        {
            _auth = auth;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult> login([FromBody] AuthDto authDto)
        {
            User? user = await _auth.Validate(authDto);

            if (user == null)
            {
                return BadRequest(
                    new { Leyenda = "Error en las credenciales", Respuesta = "Error" }
                );
            }
            else
            {
                return Ok(JsonConvert.SerializeObject(createToken(user!)));
            }
        }

        [HttpPost("Signin")]
        public async Task<ActionResult<string>> signin([FromBody] AuthDto authDto)
        {
            return await _auth.Signin(authDto);
        }

        private string createToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Name, user.username!)
            };
            var key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(
                    _configuration.GetSection("AppSettings:Token").Value
                )
            );
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
