using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VentasLimpieza.core.Dtos;
using VentasLimpieza.Core.Enum;
using VentasLimpieza.Services.Interfaces;

namespace VentasLimpieza.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }
        
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
        
        [HttpGet]
        public async Task<IActionResult> ObtenerPedidos()
        {
            var result = await _pedidoService.ObtenerPedidosAsync();
            return Ok(result);
        }
        [Authorize(Roles = nameof(RoleType.Administrator))]
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPedidoPorId(int id)
        {
            var result = await _pedidoService.ObtenerPedidoPorIdAsync(id);
            return Ok(result);
        }
        [Authorize(Roles = nameof(RoleType.Administrator))]
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
        [Authorize(Roles = nameof(RoleType.Administrator))]
        [HttpGet("resumen-general")]
        public async Task<IActionResult> GetResumenGeneralVentas()
        {
            var result = await _pedidoService.GetResumenGeneralVentasAsync();
            return Ok(result);
        }
    }
}
