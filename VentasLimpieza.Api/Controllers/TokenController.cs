using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Services.Interfaces;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ISecurityService _securityService;

        public TokenController(IConfiguration configuration,
            ISecurityService securityService)
        {
            _configuration = configuration;
            _securityService = securityService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            //Si es un usuario es válido generar el token
            var validation = await IsValidUser(userLogin);
            if (validation.Item1)
            {
                var token = GenerateToken(validation.Item2);
                return Ok(new { token });
            }

            return Unauthorized();
        }

        private async Task<(bool, Security)> IsValidUser(UserLogin userLogin)
        {
            var user = await _securityService.GetLoginByCredentials(userLogin);
            return (user != null, user);
        }

        private string GenerateToken(Security security)
        {
            //HEADER
            var symmetricSecurityKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    _configuration["Authentication:SecretKey"]));
            var signingCredentials =
                new SigningCredentials(symmetricSecurityKey,
                SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //PAYLOAD (Cuerpo)
            var claims = new[]
            {
                new Claim("Name", security.Name),
                new Claim("Login", security.Login),
                new Claim(ClaimTypes.Role, security.Role.ToString())
            };

            var payload =
                new JwtPayload(
                    //Quien emite el token (ej: https://api.ucb.edu.bo)
                    issuer: _configuration["Authentication:Issuer"],

                    //Quien va a recibir el token (ej: https://frontend.ucb.edu.bo)
                    audience: _configuration["Authentication:Audience"],

                    //los datos del usuario
                    claims: claims,

                    //Desde cuando es valido el token (ahora mismo)
                    notBefore: DateTime.UtcNow,

                    //Cuando expira
                    expires: DateTime.UtcNow.AddMinutes(2)
                    );

            //FIRMA
            var token =
                new JwtSecurityToken(header, payload);

            //Serializar el token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}