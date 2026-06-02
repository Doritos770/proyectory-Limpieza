using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using VentasLimpieza.core.Dtos;
using VentasLimpieza.Core.Enum;
using VentasLimpieza.Services.Interfaces;

namespace VentasLimpieza.Api.Controllers
{
  //  [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }
        
        /// <summary>
        /// Crea un nuevo pedido para un usuario autenticado.
        /// </summary>
        /// <remarks>Este método recibe los detalles del pedido mediante un <see cref="CrearPedidoDto"/> y valida 
        /// el modelo. Si es válido, lo envía a <see cref="IPedidoService"/> para registrar el pedido y sus detalles.</remarks>
        /// <param name="pedidoDto">Objeto que contiene los datos del pedido a crear (ej. lista de productos, cantidades).</param>
        /// <returns>Un <see cref="IActionResult"/> que contiene el pedido recién creado y la ruta para acceder a él.</returns>
        /// <response code="201">El pedido fue creado exitosamente.</response>
        /// <response code="400">Si el modelo enviado es inválido.</response>
        /// <response code="401">Si el usuario no está autenticado.</response>
        /// <response code="500">Error interno del servidor al crear el pedido.</response>
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> CrearPedido([FromBody] CrearPedidoDto pedidoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _pedidoService.CrearPedidoAsync(pedidoDto);
            return CreatedAtAction(nameof(ObtenerPedidoPorId), new { id = result.Id }, result);
        }
        
        /// <summary>
        /// Obtiene la lista de todos los pedidos registrados en el sistema.
        /// </summary>
        /// <remarks>Devuelve una colección de pedidos con la información básica. Requiere autenticación.</remarks>
        /// <returns>Un <see cref="IActionResult"/> que contiene una lista de pedidos.</returns>
        /// <response code="200">Retorna la lista de pedidos encontrados.</response>
        /// <response code="401">Si el usuario no está autenticado.</response>
        /// <response code="500">Error interno del servidor.</response>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> ObtenerPedidos()
        {
            var result = await _pedidoService.ObtenerPedidosAsync();
            return Ok(result);
        }
        /// <summary>
        /// Obtiene la información detallada de un pedido específico por su ID.
        /// </summary>
        /// <remarks>Este método busca el pedido mediante su identificador único. Solamente accesible para roles de Administrador.</remarks>
        /// <param name="id">El identificador único del pedido a buscar.</param>
        /// <returns>Un <see cref="IActionResult"/> que contiene los detalles del pedido.</returns>
        /// <response code="200">Retorna el objeto del pedido encontrado.</response>
        /// <response code="401">Si el usuario no está autenticado.</response>
        /// <response code="403">Si el usuario no tiene rol de Administrador.</response>
        /// <response code="404">Si no existe un pedido con el ID proporcionado.</response>
        /// <response code="500">Error interno del servidor.</response>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [Authorize(Roles = nameof(RoleType.Administrator))]
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPedidoPorId(int id)
        {
            var result = await _pedidoService.ObtenerPedidoPorIdAsync(id);
            return Ok(result);
        }
        /// <summary>
        /// Actualiza el estado de un pedido (ej. de 'Pendiente' a 'Enviado' o 'Entregado').
        /// </summary>
        /// <remarks>Cambia el estatus de un pedido existente. Solamente accesible para roles de Administrador.</remarks>
        /// <param name="id">El identificador único del pedido a actualizar.</param>
        /// <param name="estado">El nuevo estado (string) a asignar al pedido.</param>
        /// <returns>Un <see cref="IActionResult"/> con estado 204 (NoContent) indicando éxito.</returns>
        /// <response code="204">El estado del pedido fue actualizado exitosamente.</response>
        /// <response code="400">Si el estado proporcionado está vacío o no es válido.</response>
        /// <response code="401">Si el usuario no está autenticado.</response>
        /// <response code="403">Si el usuario no tiene rol de Administrador.</response>
        /// <response code="404">Si no se encontró el pedido con el ID especificado.</response>
        /// <response code="500">Error interno del servidor.</response>
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPut("{id}/estado")]
        public async Task<IActionResult> ActualizarEstadoPedido(int id, [FromBody] string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
            {
                return BadRequest("El estado no puede estar vacío.");
            }

            await _pedidoService.ActualizarEstadoPedidoAsync(id, estado);
            return NoContent();
        }
        /// <summary>
        /// Obtiene un resumen general estadístico de las ventas registradas.
        /// </summary>
        /// <remarks>Calcula totales y métricas de ventas. Solamente accesible para roles de Administrador.</remarks>
        /// <returns>Un <see cref="IActionResult"/> que contiene el resumen con las estadísticas de ventas.</returns>
        /// <response code="200">Retorna los datos del resumen de ventas.</response>
        /// <response code="401">Si el usuario no está autenticado.</response>
        /// <response code="403">Si el usuario no tiene rol de Administrador.</response>
        /// <response code="500">Error interno del servidor al procesar las métricas.</response>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("resumen-general")]
        public async Task<IActionResult> GetResumenGeneralVentas()
        {
            var result = await _pedidoService.GetResumenGeneralVentasAsync();
            return Ok(result);
        }
    }
}
