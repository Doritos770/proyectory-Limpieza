using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Responses;
using VentasLimpieza.core.Dtos;
using VentasLimpieza.Core.Auxiliares;
using VentasLimpieza.Core.CustomEntities;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.QueryFilter;
using VentasLimpieza.Core.Exceptions;
using VentasLimpieza.Services.Interfaces;
using VentasLimpieza.Services.Validators;

namespace VentasLimpieza.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioServices;
        private readonly IMapper _mapper;
        private readonly UsuarioDtoValidator _usuarioDtoValidator;
        public UsuarioController(
             IUsuarioService usuarioServices,
             IMapper mapper,
             UsuarioDtoValidator usuarioDtoValidator)
        {
            _usuarioServices = usuarioServices;
            _mapper = mapper;
            _usuarioDtoValidator = usuarioDtoValidator;
        }

        [HttpGet("{id}/estadisticas-pedidos")]
        public async Task<IActionResult> GetEstadisticasPedidos(int id)
        {
            try
            {
                var estadisticas = await _usuarioServices.GetEstadisticasPedidosAsync(id);
                return Ok(estadisticas);
            }
            catch (BussinesExeption ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //#region sin dtos
        //[HttpGet]
        //public async Task<IActionResult> GetUsuario()
        //{
        //    var usuarios = await _usuarioServices.GetAllUsersAsync();
        //    return Ok(usuarios);

        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetUsuarioById(int id)
        //{
        //    var usuario = await _usuarioServices.GetUsuarioByIdAsync(id);
        //    return Ok(usuario);
        //}

        //[HttpPost]
        //public async Task<IActionResult> InsertUsuario(Usuario usuario)
        //{
        //    await _usuarioServices.RegistrarUsuario(usuario);
        //    return Created($"api/usuario/{usuario.Id}", usuario);
        //}

        //[HttpPut]
        //public async Task<IActionResult> UpdateUsuario(Usuario usuario)
        //{
        //     _usuarioServices.UpdateUsuario(usuario);
        //    return NoContent();
        //}

        //[HttpDelete]
        //public async Task<IActionResult> DeleteUsuario(Usuario usuario)
        //{
        //    await _usuarioServices.DeleteUsuario(usuario.Id);
        //    return NoContent();
        //}
        //#endregion

        //#region conDtos
        //[HttpGet("dto")]
        //public async Task<IActionResult> GetDtoUsuario()
        //{
        //    var usuarios = await _usuarioServices.GetAllUsersAsync();
        //    var usuariosDto = usuarios.Select(u => new UsuarioDto
        //    {
        //        Id = u.Id,
        //        Email = u.Email,
        //        Password = u.Password,
        //        Nombre = u.Nombre,
        //        Apellido = u.Apellido,
        //        Telefono = u.Telefono,
        //        FechaRegistro = u.FechaRegistro,
        //        IsActive = u.IsActive
        //    });
        //    return Ok(usuariosDto);
        //}

        //[HttpGet("dto/{id}")]
        //public async Task<IActionResult> GetDtoUsuarioById(int id)
        //{
        //    var usuario = await _usuarioServices.GetUsuarioByIdAsync(id);
        //    var usuarioDto = new UsuarioDto
        //    {
        //        Id = usuario.Id,
        //        Email = usuario.Email,
        //        Password = usuario.Password,
        //        Nombre = usuario.Nombre,
        //        Apellido = usuario.Apellido,
        //        Telefono = usuario.Telefono,
        //        FechaRegistro = usuario.FechaRegistro,
        //        IsActive = usuario.IsActive
        //    };
        //    return Ok(usuarioDto);
        //}

        //[HttpPost("dto")]
        //public async Task<IActionResult> InsertDtoUsuario(UsuarioDto usuarioDto)
        //{
        //    var usuario = new Usuario
        //    {
        //        Id = usuarioDto.Id,
        //        Email = usuarioDto.Email,
        //        Password = usuarioDto.Password,
        //        Nombre = usuarioDto.Nombre,
        //        Apellido = usuarioDto.Apellido,
        //        Telefono = usuarioDto.Telefono,
        //        FechaRegistro = usuarioDto.FechaRegistro,
        //        IsActive = usuarioDto.IsActive
        //    };
        //    await _usuarioServices.RegistrarUsuario(usuario);
        //    return Created($"api/usuario/{usuario.Id}", usuario);
        //}

        //[HttpPut("dto/{id}")]
        //public async Task<IActionResult> UpdateDtoUsuario(int id, [FromBody] UsuarioDto usuarioDto)
        //{
        //    if (id != usuarioDto.Id)
        //        return BadRequest("El ID del usuario no coincide");

        //    var usuario = await _usuarioServices.GetUsuarioByIdAsync(id);
        //    if (usuario == null)
        //        return NotFound("Usuario no encontrado");

        //    // Mapear valor del DTO a la entidad
        //    usuario.Email = usuarioDto.Email;
        //    usuario.Password = usuarioDto.Password;
        //    usuario.Nombre = usuarioDto.Nombre;
        //    usuario.Apellido = usuarioDto.Apellido;
        //    usuario.Telefono = usuarioDto.Telefono;
        //    usuario.FechaRegistro = usuarioDto.FechaRegistro;
        //    usuario.IsActive = usuarioDto.IsActive;

        //     _usuarioServices.UpdateUsuario(usuario);
        //    return NoContent();
        //}

        //[HttpDelete("dto/{id}")]
        //public async Task<IActionResult> DeleteDtoUsuario(int id)
        //{
        //    var usuario = await _usuarioServices.GetUsuarioByIdAsync(id);
        //    if (usuario == null)
        //        return NotFound("Usuario no encontrado");

        //    await _usuarioServices.DeleteUsuario(usuario.Id);
        //    return NoContent();
        //}
        //#endregion

        #region Mapper
        [HttpGet("dto/mapper")]
        //?userId=1
        public async Task<IActionResult> GetDtoMapperUsuario(
            [FromQuery] UsuarioQueryFilter? filters)
        {
            var usuarios = await _usuarioServices.GetAllUsersAsync(filters);
            var usuariosDto = _mapper.Map<IEnumerable<UsuarioDto>>(usuarios.Pagination);
            var pagination = new Pagination
            {
                TotalCount = usuarios.Pagination.TotalCount,
                PageSize = usuarios.Pagination.PageSize,
                CurrentePage = usuarios.Pagination.CurrentPage,
                TotalPages = usuarios.Pagination.TotalPages,
                HasNextPage = usuarios.Pagination.HasNextPage,
                HasPreviousPage = usuarios.Pagination.HasPreviousPage
            };

            var response = new ApiResponse<IEnumerable<UsuarioDto>>(usuariosDto)
            {
                Pagination = pagination,
                Messages = usuarios.Messages
            };

            return StatusCode((int)usuarios.StatusCode, response);

            //var response = new ApiResponse<IEnumerable<UsuarioDto>>(usuariosDto);
            //return Ok(response);
        }
        
        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetDtoMapperUsuarioById(int id)
        {
            var usuario = await _usuarioServices.GetUsuarioByIdAsync(id);
            if (usuario == null)
                return NotFound(new { mensaje = "Usuario no encontrado" });

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
            var response = new ApiResponse<UsuarioDto>(usuarioDto);
            return Ok(response);
        }

        [HttpPost("dto/mapper")]
        public async Task<IActionResult> InsertDtoMapperUsuario(UsuarioDto usuarioDto)
        {
            // Validar el DTO
            var validationResult = await _usuarioDtoValidator.ValidateAsync(usuarioDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    message = "Error de validación",
                    errors = validationResult.Errors.Select(e => new
                    {
                        field = e.PropertyName,
                        error = e.ErrorMessage
                    })
                });
            }

            try
            {
                var usuario = _mapper.Map<Usuario>(usuarioDto);
                await _usuarioServices.RegistrarUsuario(usuario);

                var response = new ApiResponse<UsuarioDto>(usuarioDto);
                return Created($"api/usuario/{usuario.Id}", response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al registrar el usuario",
                    error = ex.Message
                });
            }
        }

        [HttpPut("dto/mapper/{id}")]
        public async Task<IActionResult> UpdateDtoMapperUsuario(int id, [FromBody] UsuarioDto usuarioDto)
        {
            if (id != usuarioDto.Id)
                return BadRequest("El ID del usuario no coincide");

            
            var validationResult = await _usuarioDtoValidator.ValidateAsync(usuarioDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    message = "Error de validación",
                    errors = validationResult.Errors.Select(e => new
                    {
                        field = e.PropertyName,
                        error = e.ErrorMessage
                    })
                });
            }

            var usuario = await _usuarioServices.GetUsuarioByIdAsync(id);
            if (usuario == null)
                return NotFound("Usuario no encontrado");

            try
            {
                _mapper.Map(usuarioDto, usuario);
                 _usuarioServices.UpdateUsuario(usuario);

                var response = new ApiResponse<UsuarioDto>(usuarioDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al actualizar el usuario",
                    error = ex.Message
                });
            }
        }

        [HttpDelete("dto/mapper/{id}")]
        public async Task<IActionResult> DeleteDtoMapperUsuario(int id)
        {
            var usuario = await _usuarioServices.GetUsuarioByIdAsync(id);
            if (usuario == null)
                return NotFound("Usuario no encontrado");

            await _usuarioServices.DeleteUsuario(usuario.Id);
            return NoContent();
        }
        #endregion

        #region BajaLogica
        [HttpPut("dto/mapper/desactivar/{id}")]
        public async Task<IActionResult> DesactivarUsuario(int id)
        {
            try
            {
                await _usuarioServices.DesactivarUsuario(id);
                var response = new ApiResponse<string>("Usuario desactivado exitosamente");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al desactivar el usuario",
                    error = ex.Message
                });
            }
        }

        [HttpPut("dto/mapper/activar/{id}")]
        public async Task<IActionResult> ActivarUsuario(int id)
        {
            try
            {
                await _usuarioServices.ActivarUsuario(id);
                var response = new ApiResponse<string>("Usuario reactivado exitosamente");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al reactivar el usuario",
                    error = ex.Message
                });
            }
        }

        [HttpGet("dto/mapper/inactivos")]
        public async Task<IActionResult> GetUsuariosInactivos()
        {
            try
            {
                var usuarios = await _usuarioServices.GetUsuariosInactivosAsync();
                var usuariosDto = _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
                var response = new ApiResponse<IEnumerable<UsuarioDto>>(usuariosDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al obtener usuarios inactivos",
                    error = ex.Message
                });
            }
        }
        #endregion

        #region Dapper
        [HttpGet("dto/mapper/dapper/top-pedidos")]
        public async Task<IActionResult> GetTopUsuariosPedidos()
        {
            try
            {
                var usuarios = await _usuarioServices.GetUsuariosConMasPedidosAsync();
                var response = new ApiResponse<IEnumerable<UsuarioPedidosSimple>>(usuarios);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al obtener usuarios con más pedidos",
                    error = ex.Message
                });
            }
        }

        [HttpGet("dto/mapper/dapper/{id}/resenas")]
        public async Task<IActionResult> GetResenasUsuario(int id)
        {
            try
            {
                var resenas = await _usuarioServices.GetUsuariosResena(id);
                var response = new ApiResponse<IEnumerable<ResenaSimple>>(resenas);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al obtener reseñas del usuario",
                    error = ex.Message
                });
            }
        }
        #endregion

        #region Contrasena
        [HttpPost("dto/mapper/solicitar-codigo")]
        public async Task<IActionResult> SolicitarCodigoCambioContrasena([FromBody] SolicitudCambiodeContrasena solicitud)
        {
            try
            {
                var codigo = await _usuarioServices.SolicitudCodigo(solicitud);
                var response = new ApiResponse<string>($"Código de verificación generado exitosamente");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al solicitar código",
                    error = ex.Message
                });
            }
        }

        [HttpPut("dto/mapper/actualizar-contrasena")]
        public async Task<IActionResult> ActualizarContrasena([FromBody] NuevaContrasena nuevaContrasena)
        {
            try
            {
                var resultado = await _usuarioServices.ActualizarContrasena(nuevaContrasena);

                if (resultado)
                {
                    var response = new ApiResponse<string>("Contraseña actualizada exitosamente");
                    return Ok(response);
                }
                else
                {
                    return BadRequest(new { message = "Código de verificación inválido" });
                }
            }
            catch (BussinesExeption ex)
            {
                return StatusCode((int)ex.StatusCode, new
                {
                    message = ex.Message,
                    details = ex.Details
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al actualizar contraseña",
                    error = ex.Message
                });
            }
        }
        #endregion
    }
}