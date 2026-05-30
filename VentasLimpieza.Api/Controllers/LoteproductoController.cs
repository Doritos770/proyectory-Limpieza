using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Responses;
using VentasLimpieza.core.Dtos;
using VentasLimpieza.Core.Auxiliares;
using VentasLimpieza.Core.CustomEntities;
using VentasLimpieza.Core.Dtos;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.QueryFilter;
using VentasLimpieza.Services.Interfaces;
using VentasLimpieza.Services.Validators;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VentasLimpieza.Api.Controllers
{
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