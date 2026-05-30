using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Responses;
using System.Net;
using VentasLimpieza.Core.Dtos;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.Enum;
using VentasLimpieza.Services.Interfaces;

namespace VentasLimpieza.Api.Controllers
{
    
    [Authorize(Roles = nameof(RoleType.Administrator))]
    //[Authorize(Roles = $"{nameof(RoleType.Administrator)},{nameof(RoleType.Supervisor)}")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public SecurityController(ISecurityService securityService,
            IMapper mapper,
            IPasswordService passwordService)
        {
            _securityService = securityService;
            _mapper = mapper;
            _passwordService = passwordService;
        }
        [AllowAnonymous]

        /// <summary>
        /// Registra un nuevo usuario en el sistema con un rol específico (Ej. Administrador o Supervisor).
        /// </summary>
        /// <remarks>Este método recibe los datos de seguridad de un usuario mediante un DTO, realiza el hash de la contraseña 
        /// mediante el <see cref="IPasswordService"/> y almacena el registro en la base de datos a través de <see cref="ISecurityService"/>.
        /// Finalmente, retorna el DTO del usuario recién creado encapsulado en un <see cref="ApiResponse{T}"/>.</remarks>
        /// <param name="securityDto">Objeto que contiene las credenciales (Login, Password), nombre y rol del nuevo usuario.</param>
        /// <returns>Un <see cref="IActionResult"/> que contiene un <see cref="ApiResponse{T}"/> con el objeto <see cref="SecurityDto"/> 
        /// registrado exitosamente.</returns>
        /// <response code="200">Retorna el <see cref="SecurityDto"/> del usuario creado.</response>
        /// <response code="400">Si la petición es inválida o el formato es incorrecto.</response>
        /// <response code="500">Error interno del servidor al procesar el registro.</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<SecurityDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post(SecurityDto securityDto)
        {
            var security = _mapper.Map<Security>(securityDto);
            security.Password = _passwordService.Hash(security.Password);
            await _securityService.RegisterUser(security);

            securityDto = _mapper.Map<SecurityDto>(security);
            var response = new ApiResponse<SecurityDto>(securityDto);
            return Ok(response);
        }
    }
}
