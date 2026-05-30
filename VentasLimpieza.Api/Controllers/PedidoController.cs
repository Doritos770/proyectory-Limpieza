using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VentasLimpieza.core.Dtos;
using VentasLimpieza.Services.Interfaces;

namespace VentasLimpieza.Api.Controllers
{
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

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPedidoPorId(int id)
        {
            var result = await _pedidoService.ObtenerPedidoPorIdAsync(id);
            return Ok(result);
        }

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

        [HttpGet("resumen-general")]
        public async Task<IActionResult> GetResumenGeneralVentas()
        {
            var result = await _pedidoService.GetResumenGeneralVentasAsync();
            return Ok(result);
        }
    }
}
