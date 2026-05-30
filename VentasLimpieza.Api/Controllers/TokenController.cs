using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.Enum;
using VentasLimpieza.Services.Interfaces;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ISecurityService _securityService;
        private readonly IPasswordService _passwordService;
        public TokenController(IConfiguration configuration,
            ISecurityService securityService, IPasswordService passwordService)
        {
            _configuration = configuration;
            _securityService = securityService;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Autentica a un usuario y genera un token JWT válido.
        /// </summary>
        /// <remarks>Recibe las credenciales (usuario y contraseña) a través del modelo <see cref="UserLogin"/>, 
        /// valida su existencia y contraseña y, si es correcto, genera y retorna un token JWT que debe enviarse en la 
        /// cabecera de las siguientes peticiones como "Bearer {token}".</remarks>
        /// <param name="userLogin">Objeto que contiene el nombre de usuario y la contraseña a validar.</param>
        /// <returns>Un <see cref="IActionResult"/> que contiene el token JWT generado si las credenciales son correctas.</returns>
        /// <response code="200">Retorna el token JWT generado.</response>
        /// <response code="401">Si las credenciales provistas son inválidas.</response>
        /// <response code="500">Error interno del servidor al procesar la autenticación.</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(object))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
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
            var isValid = _passwordService.Check(user.Password, userLogin.Password);
            //return (user != null, user);
            return (isValid, user);
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
                //new Claim(ClaimTypes.Role, Enum.GetName(typeof(RoleType), security.Role))
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