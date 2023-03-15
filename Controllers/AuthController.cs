using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using App.Services.AuthService;
using App.Dto.Auth;

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
        public async Task<ActionResult<LoginDto>> login([FromBody] AuthDto authDto)
        {
            UserDto? user = await _auth.Validate(authDto);

            if (user == null)
            {
                return BadRequest(
                    new { Leyenda = "Error en las credenciales", Respuesta = "Error" }
                );
            }
            else
            {
                return new LoginDto { user = user, token = createToken(user!) };
            }
        }

        [HttpPost("signin")]
        public async Task<ActionResult> signin([FromBody] AuthDto authDto)
        {
            try
            {
                var res = await _auth.Signin(authDto);

                return new JsonResult(new { Respuesta = "ok" });
            }
            catch (System.Exception)
            {
                return BadRequest(
                    new { Leyenda = "Error en las credenciales", Respuesta = "Error" }
                );
            }
        }

        private string createToken(UserDto user)
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
