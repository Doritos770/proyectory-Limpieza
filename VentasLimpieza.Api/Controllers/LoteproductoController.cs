using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Responses;
using System.Net;
using VentasLimpieza.core.Dtos;
using VentasLimpieza.Core.Auxiliares;
using VentasLimpieza.Core.CustomEntities;
using VentasLimpieza.Core.Dtos;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.Enum;
using VentasLimpieza.Core.QueryFilter;
using VentasLimpieza.Services.Interfaces;
using VentasLimpieza.Services.Validators;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VentasLimpieza.Api.Controllers
{
    [Authorize(Roles = nameof(RoleType.Administrator))]
    [Route("api/[controller]")] // api/Loteproducto   
    [ApiController]
    public class LoteproductoController : ControllerBase
    {
        private readonly ILoteproductoService _loteproductoService;
        private readonly IMapper _mapper;
        private readonly LoteproductoDtoValidator _validator;

        public LoteproductoController(
            ILoteproductoService loteproductoService,
            IMapper mapper,
            LoteproductoDtoValidator validator)
        {
            _loteproductoService = loteproductoService;
            _mapper = mapper;
            _validator = validator;
        }

        /// <summary>
        /// Recupera una lista paginada de lotes de productos según los filtros especificados.
        /// </summary>
        /// <remarks>Este método utiliza la capa de servicio para recuperar los lotes y devuelve metadatos de paginación.</remarks>
        /// <param name="filters">Filtros opcionales para la consulta, como paginación y otros criterios específicos.</param>
        /// <returns>Un <see cref="IActionResult"/> que contiene un <see cref="ApiResponse{T}"/> con la lista de lotes y detalles de paginación.</returns>
        /// <response code="200">Retorna la lista de <see cref="LoteproductoDto"/>.</response>
        /// <response code="401">No autorizado, requiere autenticación.</response>
        /// <response code="403">No permitido, requiere rol de Administrador.</response>
        /// <response code="500">Error interno del servidor al obtener los lotes.</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<LoteproductoDto>>))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] LoteproductoQueryFilter? filters)
        {
            try
            {
                var lotes = await _loteproductoService.GetAllLoteproductosAsync(filters);
                var lotesDto = _mapper.Map<IEnumerable<LoteproductoDto>>(lotes.Pagination);
                //var response = new ApiResponse<IEnumerable<LoteproductoDto>>(lotesDto);
                //return Ok(response);
                var pagination = new Pagination
                {
                    TotalCount = lotes.Pagination.TotalCount,
                    PageSize = lotes.Pagination.PageSize,
                    CurrentePage = lotes.Pagination.CurrentPage,
                    TotalPages = lotes.Pagination.TotalPages,
                    HasNextPage = lotes.Pagination.HasNextPage,
                    HasPreviousPage = lotes.Pagination.HasPreviousPage
                };

                var response = new ApiResponse<IEnumerable<LoteproductoDto>>(lotesDto)
                {
                    Pagination = pagination,
                    Messages = lotes.Messages
                };
                return StatusCode((int)lotes.StatusCode, response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al obtener los lotes",
                    error = ex.Message
                });
            }
        }
        


        /// <summary>
        /// Obtiene un lote de producto específico por su identificador.
        /// </summary>
        /// <remarks>Devuelve el detalle completo del lote si se encuentra en la base de datos.</remarks>
        /// <param name="id">El identificador único del lote de producto.</param>
        /// <returns>Un <see cref="IActionResult"/> que contiene un <see cref="ApiResponse{T}"/> con el <see cref="LoteproductoDto"/> encontrado.</returns>
        /// <response code="200">Retorna el lote encontrado.</response>
        /// <response code="401">No autorizado, requiere autenticación.</response>
        /// <response code="403">No permitido, requiere rol de Administrador.</response>
        /// <response code="404">Si no existe el lote con el ID proporcionado.</response>
        /// <response code="500">Error interno del servidor al obtener el lote.</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<LoteproductoDto>))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var lote = await _loteproductoService.GetLoteproductoByIdAsync(id);
                if (lote == null)
                    return NotFound(new { mensaje = "Lote no encontrado" });

                var loteDto = _mapper.Map<LoteproductoDto>(lote);
                var response = new ApiResponse<LoteproductoDto>(loteDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al obtener el lote",
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// Crea un nuevo registro de lote de producto.
        /// </summary>
        /// <remarks>Realiza la validación del DTO enviado y, si es correcto, registra el nuevo lote en el sistema.</remarks>
        /// <param name="loteproductoDto">Los datos del nuevo lote a crear.</param>
        /// <returns>Un <see cref="IActionResult"/> indicando que el lote fue creado y proporcionando la ruta de acceso.</returns>
        /// <response code="201">Retorna el <see cref="LoteproductoDto"/> recién creado.</response>
        /// <response code="400">Si los datos proporcionados son inválidos.</response>
        /// <response code="401">No autorizado, requiere autenticación.</response>
        /// <response code="403">No permitido, requiere rol de Administrador.</response>
        /// <response code="500">Error interno del servidor al registrar el lote.</response>
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(ApiResponse<LoteproductoDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LoteproductoDto loteproductoDto)
        {
            // Validación
            var validationResult = await _validator.ValidateAsync(loteproductoDto);
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
                var lote = _mapper.Map<Loteproducto>(loteproductoDto);
                await _loteproductoService.RegistrarLoteproducto(lote);

                var response = new ApiResponse<LoteproductoDto>(loteproductoDto);
                return Created($"api/loteproducto/{lote.Id}", response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al registrar el lote",
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// Actualiza la información de un lote de producto existente.
        /// </summary>
        /// <remarks>Valida el modelo y comprueba la existencia del lote antes de aplicar los cambios.</remarks>
        /// <param name="id">El identificador único del lote a actualizar.</param>
        /// <param name="loteproductoDto">Objeto con la información actualizada del lote.</param>
        /// <returns>Un <see cref="IActionResult"/> que contiene el lote actualizado.</returns>
        /// <response code="200">Retorna el lote actualizado.</response>
        /// <response code="400">Si la información enviada no pasa las validaciones de negocio.</response>
        /// <response code="401">No autorizado, requiere autenticación.</response>
        /// <response code="403">No permitido, requiere rol de Administrador.</response>
        /// <response code="404">Si no existe un lote con el ID especificado.</response>
        /// <response code="500">Error interno del servidor al procesar la actualización.</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<LoteproductoDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LoteproductoDto loteproductoDto)
        {
            // Validación
            var validationResult = await _validator.ValidateAsync(loteproductoDto);
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
                // Verificar existencia
                var loteExistente = await _loteproductoService.GetLoteproductoByIdAsync(id);
                if (loteExistente == null)
                    return NotFound(new { mensaje = $"No se encontró el lote con ID {id}" });

                var lote = _mapper.Map<Loteproducto>(loteproductoDto);
                lote.Id = id;

                 _loteproductoService.UpdateLoteproducto(lote);

                var response = new ApiResponse<LoteproductoDto>(loteproductoDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al actualizar el lote",
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// Elimina un lote de producto del sistema (Soft Delete o borrado físico, según la capa de servicios).
        /// </summary>
        /// <remarks>Busca la existencia del lote y lo elimina si es encontrado.</remarks>
        /// <param name="id">El identificador único del lote a eliminar.</param>
        /// <returns>Un <see cref="IActionResult"/> indicando que la operación de eliminación fue exitosa.</returns>
        /// <response code="204">El lote se eliminó exitosamente (Sin contenido de respuesta).</response>
        /// <response code="401">No autorizado, requiere autenticación.</response>
        /// <response code="403">No permitido, requiere rol de Administrador.</response>
        /// <response code="404">Si no se encontró el lote a eliminar.</response>
        /// <response code="500">Error interno del servidor al procesar la eliminación.</response>
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var lote = await _loteproductoService.GetLoteproductoByIdAsync(id);
                if (lote == null)
                    return NotFound(new { mensaje = "Lote no encontrado" });

                await _loteproductoService.DeleteLoteproducto(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al eliminar el lote",
                    error = ex.Message
                });
            }
        }

        #region Dapper
        /// <summary>
        /// Obtiene un resumen simple de los lotes de productos que han superado su fecha de caducidad.
        /// </summary>
        /// <remarks>Este método utiliza consultas optimizadas (Dapper) para obtener registros rápidamente.</remarks>
        /// <returns>Un <see cref="IActionResult"/> que contiene un listado simple de los lotes caducados.</returns>
        /// <response code="200">Retorna la lista de lotes caducados.</response>
        /// <response code="401">No autorizado, requiere autenticación.</response>
        /// <response code="403">No permitido, requiere rol de Administrador.</response>
        /// <response code="500">Error interno del servidor al realizar la consulta de caducados.</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<LoteproductoSimple>>))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("dapper/caducados")]
        public async Task<IActionResult> GetLotesCaducados()
        {
            try
            {
                var lotes = await _loteproductoService.GetLotesCaducadosAsync();
                var response = new ApiResponse<IEnumerable<LoteproductoSimple>>(lotes);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al obtener lotes caducados",
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// Obtiene un listado de lotes que poseen una cantidad de stock inferior o igual al mínimo especificado.
        /// </summary>
        /// <remarks>Utiliza consultas de alta eficiencia vía Dapper para recuperar información crítica de inventario.</remarks>
        /// <param name="stockMinimo">El umbral mínimo de stock para considerar el lote en alerta (por defecto 5).</param>
        /// <returns>Un <see cref="IActionResult"/> con un listado simple de los lotes en alerta de stock.</returns>
        /// <response code="200">Retorna la lista de lotes con bajo stock.</response>
        /// <response code="401">No autorizado, requiere autenticación.</response>
        /// <response code="403">No permitido, requiere rol de Administrador.</response>
        /// <response code="500">Error interno del servidor.</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<LoteproductoSimple>>))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("dapper/bajo-stock")]
        public async Task<IActionResult> GetLotesBajoStock([FromQuery] int stockMinimo = 5)
        {
            try
            {
                var lotes = await _loteproductoService.GetLotesBajoStockAsync(stockMinimo);
                var response = new ApiResponse<IEnumerable<LoteproductoSimple>>(lotes);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al obtener lotes con bajo stock",
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// Obtiene un listado de lotes que están próximos a caducar dentro de un rango de días.
        /// </summary>
        /// <remarks>Consulta rápida usando Dapper para anticipar vencimientos de inventario.</remarks>
        /// <param name="dias">El número de días hacia el futuro para considerar la proximidad (por defecto 30).</param>
        /// <returns>Un <see cref="IActionResult"/> con los lotes próximos a caducar.</returns>
        /// <response code="200">Retorna la lista de lotes próximos a caducar.</response>
        /// <response code="401">No autorizado, requiere autenticación.</response>
        /// <response code="403">No permitido, requiere rol de Administrador.</response>
        /// <response code="500">Error interno del servidor.</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<LoteproductoSimple>>))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("dapper/proximos-caducar")]
        public async Task<IActionResult> GetLotesProximosACaducar([FromQuery] int dias = 30)
        {
            try
            {
                var lotes = await _loteproductoService.GetLotesProximosACaducarAsync(dias);
                var response = new ApiResponse<IEnumerable<LoteproductoSimple>>(lotes);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al obtener lotes próximos a caducar",
                    error = ex.Message
                });
            }
        }
        #endregion

        #region ProcesosMasivos
        /// <summary>
        /// Inactiva de forma masiva todos los lotes de productos cuya fecha de caducidad ha expirado.
        /// </summary>
        /// <remarks>Este proceso masivo actualiza el estado de los lotes caducados, sacándolos del inventario activo.</remarks>
        /// <returns>Un <see cref="IActionResult"/> indicando la cantidad de lotes que fueron dados de baja.</returns>
        /// <response code="200">El proceso se ejecutó exitosamente, retorna la cantidad de lotes afectados.</response>
        /// <response code="401">No autorizado, requiere autenticación.</response>
        /// <response code="403">No permitido, requiere rol de Administrador.</response>
        /// <response code="500">Error interno del servidor durante la ejecución masiva.</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPut("procesos/dar-baja-caducados")]
        public async Task<IActionResult> DarBajaLotesCaducados()
        {
            try
            {
                var cantidad = await _loteproductoService.DarBajaLotesCaducadosAsync();
                var response = new ApiResponse<object>(new
                {
                    mensaje = $"Se dieron de baja {cantidad} lotes caducados",
                    cantidadLotesInactivados = cantidad
                });
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al dar de baja lotes caducados",
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// Proceso masivo para identificar y marcar (generar alertas) lotes con bajo stock.
        /// </summary>
        /// <remarks>Recorre el inventario actualizando el estatus o marcando alertas para lotes cuyo stock sea inferior al mínimo especificado.</remarks>
        /// <param name="stockMinimo">El umbral mínimo de stock (por defecto 5).</param>
        /// <returns>Un <see cref="IActionResult"/> con el total de lotes marcados.</returns>
        /// <response code="200">El proceso se ejecutó exitosamente.</response>
        /// <response code="401">No autorizado, requiere autenticación.</response>
        /// <response code="403">No permitido, requiere rol de Administrador.</response>
        /// <response code="500">Error interno del servidor al procesar el marcado masivo.</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPut("procesos/marcar-bajo-stock")]
        public async Task<IActionResult> MarcarLotesBajoStock([FromQuery] int stockMinimo = 5)
        {
            try
            {
                var cantidad = await _loteproductoService.MarcarLotesBajoStockAsync(stockMinimo);
                var response = new ApiResponse<object>(new
                {
                    mensaje = $"Se marcaron {cantidad} lotes con bajo stock",
                    cantidadLotesBajoStock = cantidad
                });
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al marcar lotes con bajo stock",
                    error = ex.Message
                });
            }
        }
        #endregion
    }
}