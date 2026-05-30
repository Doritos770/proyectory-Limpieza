using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Responses;
using VentasLimpieza.core.Dtos;
using VentasLimpieza.Core.Auxiliares;
using VentasLimpieza.Core.CustomEntities;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.QueryFilter;
using VentasLimpieza.Core.Exceptions;
using System.Net;
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

        /// <summary>
        /// Obtiene un reporte estadístico de los pedidos de un usuario específico.
        /// </summary>
        /// <remarks>Consulta la información transaccional y devuelve métricas del comportamiento de compra del usuario.</remarks>
        /// <param name="id">El identificador único del usuario.</param>
        /// <returns>Un <see cref="IActionResult"/> que contiene estadísticas de pedidos para el usuario.</returns>
        /// <response code="200">Retorna los datos estadísticos del usuario.</response>
        /// <response code="400">Si hay un error en la lógica de negocio al obtener las estadísticas.</response>
        /// <response code="500">Error interno del servidor.</response>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
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
        /// <summary>
        /// Recupera una lista paginada de todos los usuarios registrados según los filtros provistos.
        /// </summary>
        /// <remarks>Este método devuelve los usuarios mapeados en objetos <see cref="UsuarioDto"/> e incluye información de paginación detallada.</remarks>
        /// <param name="filters">Filtros de búsqueda y criterios de paginación.</param>
        /// <returns>Un <see cref="IActionResult"/> que contiene un <see cref="ApiResponse{T}"/> con el catálogo de usuarios.</returns>
        /// <response code="200">Retorna la colección paginada de usuarios.</response>
        /// <response code="500">Error interno del servidor.</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<UsuarioDto>>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
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
        
        /// <summary>
        /// Obtiene la información detallada de un usuario por su ID.
        /// </summary>
        /// <remarks>Realiza una búsqueda del usuario y lo devuelve mapeado como <see cref="UsuarioDto"/>.</remarks>
        /// <param name="id">El identificador numérico del usuario.</param>
        /// <returns>Un <see cref="IActionResult"/> con el usuario encontrado.</returns>
        /// <response code="200">Retorna el usuario solicitado.</response>
        /// <response code="404">Si no existe ningún usuario asociado al ID enviado.</response>
        /// <response code="500">Error interno del servidor.</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<UsuarioDto>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
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

        /// <summary>
        /// Registra un nuevo usuario utilizando DTOs.
        /// </summary>
        /// <remarks>Valida el <see cref="UsuarioDto"/> recibido mediante <see cref="UsuarioDtoValidator"/> y persiste la información si es correcta.</remarks>
        /// <param name="usuarioDto">Datos requeridos para la creación del usuario.</param>
        /// <returns>Un <see cref="IActionResult"/> indicando que el registro se realizó con éxito.</returns>
        /// <response code="201">Retorna el usuario recién creado.</response>
        /// <response code="400">Si existen errores de validación en la información enviada.</response>
        /// <response code="500">Error interno del servidor al crear el usuario.</response>
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(ApiResponse<UsuarioDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
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

        /// <summary>
        /// Actualiza la información de un usuario existente.
        /// </summary>
        /// <remarks>Sobrescribe la información del usuario siempre que el DTO pase las validaciones de negocio correspondientes.</remarks>
        /// <param name="id">El ID del usuario a modificar.</param>
        /// <param name="usuarioDto">El objeto DTO con la información actualizada.</param>
        /// <returns>Un <see cref="IActionResult"/> indicando el resultado de la actualización.</returns>
        /// <response code="200">El usuario fue actualizado exitosamente.</response>
        /// <response code="400">Si el ID no coincide o los datos son inválidos.</response>
        /// <response code="404">Si no se encuentra un usuario con ese ID.</response>
        /// <response code="500">Error interno del servidor al procesar la actualización.</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<UsuarioDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
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

        /// <summary>
        /// Elimina un usuario del sistema (borrado físico o lógico según capa de servicios).
        /// </summary>
        /// <remarks>Busca el usuario especificado y lo elimina de la base de datos si existe.</remarks>
        /// <param name="id">El ID del usuario a eliminar.</param>
        /// <returns>Un <see cref="IActionResult"/> indicando si la operación fue exitosa.</returns>
        /// <response code="204">El usuario fue eliminado correctamente (sin contenido devuelto).</response>
        /// <response code="404">Si no se encuentra el usuario con el ID especificado.</response>
        /// <response code="500">Error interno del servidor al intentar borrar al usuario.</response>
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
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
        /// <summary>
        /// Cambia el estado de un usuario a Inactivo (Baja lógica).
        /// </summary>
        /// <remarks>En lugar de eliminar físicamente al usuario, se marca como inactivo en el sistema.</remarks>
        /// <param name="id">El ID del usuario a desactivar.</param>
        /// <returns>Un <see cref="IActionResult"/> confirmando la desactivación.</returns>
        /// <response code="200">El usuario se desactivó correctamente.</response>
        /// <response code="500">Error interno del servidor al intentar desactivar al usuario.</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
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

        /// <summary>
        /// Restaura el estado de un usuario inactivo a Activo.
        /// </summary>
        /// <remarks>Permite habilitar nuevamente el acceso a un usuario previamente dado de baja.</remarks>
        /// <param name="id">El ID del usuario a reactivar.</param>
        /// <returns>Un <see cref="IActionResult"/> confirmando la reactivación.</returns>
        /// <response code="200">El usuario se activó correctamente.</response>
        /// <response code="500">Error interno del servidor al intentar reactivar al usuario.</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
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

        /// <summary>
        /// Obtiene un listado de todos los usuarios inactivos (dados de baja lógicamente).
        /// </summary>
        /// <remarks>Filtra y devuelve usuarios cuyo estado `IsActive` sea false.</remarks>
        /// <returns>Un <see cref="IActionResult"/> con la lista de usuarios inactivos.</returns>
        /// <response code="200">Retorna la colección de usuarios inactivos.</response>
        /// <response code="500">Error interno del servidor.</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<UsuarioDto>>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
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
        /// <summary>
        /// Obtiene el Top de usuarios con la mayor cantidad de pedidos realizados.
        /// </summary>
        /// <remarks>Utiliza una consulta optimizada (Dapper) para contabilizar pedidos y rankear a los clientes más frecuentes.</remarks>
        /// <returns>Un <see cref="IActionResult"/> con el listado simple de usuarios y su cantidad de pedidos.</returns>
        /// <response code="200">Retorna el ranking de usuarios por número de pedidos.</response>
        /// <response code="500">Error interno del servidor al ejecutar la consulta.</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<UsuarioPedidosSimple>>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
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

        /// <summary>
        /// Obtiene todas las reseñas (calificaciones y comentarios) dejadas por un usuario específico.
        /// </summary>
        /// <remarks>Recupera el historial de retroalimentación de un usuario utilizando Dapper para rendimiento óptimo.</remarks>
        /// <param name="id">El ID numérico del usuario.</param>
        /// <returns>Un <see cref="IActionResult"/> que contiene las reseñas del usuario.</returns>
        /// <response code="200">Retorna el listado de reseñas realizadas por el usuario.</response>
        /// <response code="500">Error interno del servidor.</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<ResenaSimple>>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
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
        /// <summary>
        /// Solicita la generación y envío de un código de verificación para cambiar la contraseña.
        /// </summary>
        /// <remarks>Inicia el flujo de recuperación de cuenta. Envía un código (ej. vía email/SMS) necesario para restablecer el acceso.</remarks>
        /// <param name="solicitud">Objeto que contiene el email u otra credencial de contacto del usuario.</param>
        /// <returns>Un <see cref="IActionResult"/> indicando que el código fue enviado exitosamente.</returns>
        /// <response code="200">El código se generó y envió con éxito.</response>
        /// <response code="500">Error interno del servidor al procesar la solicitud de código.</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
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

        /// <summary>
        /// Valida el código y actualiza la contraseña de un usuario.
        /// </summary>
        /// <remarks>Permite restablecer la contraseña utilizando un código de verificación previamente solicitado.</remarks>
        /// <param name="nuevaContrasena">Objeto con el código y la nueva contraseña deseada.</param>
        /// <returns>Un <see cref="IActionResult"/> confirmando el éxito de la operación.</returns>
        /// <response code="200">La contraseña fue actualizada correctamente.</response>
        /// <response code="400">Si el código enviado es inválido o expiró.</response>
        /// <response code="500">Error interno del servidor.</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
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