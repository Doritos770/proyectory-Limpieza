using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Responses;
using VentasLimpieza.core.Dtos;
using VentasLimpieza.Core.Auxiliares;
using VentasLimpieza.Core.CustomEntities;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.QueryFilter;
using VentasLimpieza.Services.Interfaces;

namespace VentasLimpieza.Api.Controllers
{
    [Route("api/[controller]")] // api/producto
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;
        private readonly IMapper _mapper;

        public ProductoController(
             IProductoService productoService,
             IMapper mapper)
        {
            _productoService = productoService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] ProductoQueryFilter? filters)
        {
            var productos = await _productoService.GetAllProductsAsync(filters);
            var productosDto = _mapper.Map<IEnumerable<ProductoDtoPorLote>>(productos.Pagination);

            //var response = new ApiResponse<IEnumerable<UsuarioDto>>(usuariosDto);
            //return Ok(response);
            //var response = new ApiResponse<IEnumerable<ProductoDtoPorLote>>(productosDto);
            //return Ok(response);
            var pagination = new Pagination
            {
                TotalCount = productos.Pagination.TotalCount,
                PageSize = productos.Pagination.PageSize,
                CurrentePage = productos.Pagination.CurrentPage,
                TotalPages = productos.Pagination.TotalPages,
                HasNextPage = productos.Pagination.HasNextPage,
                HasPreviousPage = productos.Pagination.HasPreviousPage
            };

            var response = new ApiResponse<IEnumerable<ProductoDtoPorLote>>(productosDto)
            {
                Pagination = pagination,
                Messages = productos.Messages
            };
            return StatusCode((int)productos.StatusCode, response);
            
            
        }

        /***************************************************************************************************/
        [HttpGet("dto/mapper/dapper/productoPorLote")]
        public async Task<IActionResult> GetPostsDtoMapperDapper
            ()
        {
            var productos = await _productoService.GetProductosPorLote();
            var productosDto = _mapper.Map<IEnumerable<ProductoPorLote>>(productos);

            var response = new ApiResponse<IEnumerable<ProductoPorLote>>(productosDto);

            return Ok(response);
        }
        [HttpGet("dto/mapper/dapper/productosSinVenta")]
        public async Task<IActionResult> GetProductosSinVenta
            ()
        {
            var productos = await _productoService.GetProductosSinVenta();
            var productosDto = _mapper.Map<IEnumerable<ProductoSinVentaDTO>>(productos);

            var response = new ApiResponse<IEnumerable<ProductoSinVentaDTO>>(productosDto);

            return Ok(response);
        }

        [HttpGet("mas-vendidos")]
        public async Task<IActionResult> GetProductosMasVendidos([FromQuery] int limit = 5)
        {
            var result = await _productoService.GetProductosMasVendidos(limit);
            return Ok(result);
        }

        [HttpGet("ganancias-por-lote")]
        public async Task<IActionResult> GetGananciasPorLote()
        {
            var result = await _productoService.GetGananciasPorLote();
            return Ok(result);
        }

        [HttpGet("dto/mapper/estadistica")]
        public async Task<IActionResult> GetEstadisticaProductoPorCategoria()
        {
            var producto = await _productoService.GetEstadisticaProductoPorCategoria();
            if (producto == null)
                return NotFound(new { mensaje = "Producto no encontrado" });
            var productoDto = _mapper.Map<ProductoDtoPorLote>(producto);
            var response = new ApiResponse<ProductoDtoPorLote>(productoDto);
            return Ok(response);
        }

        //[HttpGet("{id}")]
        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetDtoMapperProductoById(int id)
        {
            var producto = await _productoService.GetProductoByIdAsync(id);
            if (producto == null)
                return NotFound(new { mensaje = "Producto no encontrado" });
            var productoDto = _mapper.Map<ProductoDtoPorLote>(producto);
            var response = new ApiResponse<ProductoDtoPorLote>(productoDto);
            return Ok(response);
        }
        
        

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductoDtoPorLote productoDto)
        {
            var producto = _mapper.Map<Producto>(productoDto);
            await _productoService.RegistrarProducto(producto);
            return Ok();
        }

        //[HttpPut]
        //public IActionResult Update([FromBody] ProductoDtoPorLote productoDto)
        //{
        //    var producto = _mapper.Map<Producto>(productoDto);
        //    _productoService.UpdateProducto(producto);
        //    return Ok();
        //}
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductoDtoPorLote productoDto)
        {
            var producto = _mapper.Map<Producto>(productoDto);
            await _productoService.UpdateProducto(producto);  
            return Ok();  
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productoService.DeleteProducto(id);
            return Ok();
        }
    }

}
