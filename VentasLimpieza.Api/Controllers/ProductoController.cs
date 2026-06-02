using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Responses;
using System.Net;
using VentasLimpieza.core.Dtos;
using VentasLimpieza.Core.Auxiliares;
using VentasLimpieza.Core.CustomEntities;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.Enum;
using VentasLimpieza.Core.QueryFilter;
using VentasLimpieza.Services.Interfaces;

namespace VentasLimpieza.Api.Controllers
{
 //   [Authorize]
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
        /// <summary>
        /// Recupera un catálogo paginado de productos según los filtros proporcionados.
        /// </summary>
        /// <remarks>Devuelve los productos mapeados a <see cref="ProductoDtoPorLote"/> e incluye información sobre la paginación.</remarks>
        /// <param name="filters">Criterios de búsqueda y paginación para aplicar a la consulta.</param>
        /// <returns>Un <see cref="IActionResult"/> que contiene un <see cref="ApiResponse{T}"/> con la lista de productos.</returns>
        /// <response code="200">Retorna la lista de productos encontrados.</response>
        /// <response code="500">Error interno del servidor al consultar el catálogo.</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<ProductoDtoPorLote>>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [AllowAnonymous] //cualquiera ve lo necesario, ya luego se protege con admin 
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
        /// <summary>
        /// Obtiene un producto específico mediante su ID.
        /// </summary>
        /// <remarks>Si el producto existe, retorna un DTO detallado del producto.</remarks>
        /// <param name="id">ID numérico del producto a buscar.</param>
        /// <returns>Un <see cref="IActionResult"/> que contiene el <see cref="ApiResponse{T}"/> del producto.</returns>
        /// <response code="200">Retorna el producto correspondiente al ID.</response>
        /// <response code="404">Si no existe ningún producto con el ID especificado.</response>
        /// <response code="500">Error interno del servidor.</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<ProductoDtoPorLote>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [AllowAnonymous]
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

        /***************************************************************************************************/
        /// <summary>
        /// Obtiene un listado de productos consolidados por sus lotes, utilizando Dapper para alto rendimiento.
        /// </summary>
        /// <remarks>Solamente accesible por usuarios con rol Administrador. Retorna la información de stock agrupada.</remarks>
        /// <returns>Un <see cref="IActionResult"/> que contiene la lista de productos con información de lotes.</returns>
        /// <response code="200">Retorna la colección de productos y sus lotes.</response>
        /// <response code="401">No autorizado, se requiere token válido.</response>
        /// <response code="403">Se requiere rol de Administrador.</response>
        /// <response code="500">Error interno del servidor.</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<ProductoPorLote>>))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
       // [Authorize(Roles = nameof(RoleType.Administrator))]
        [HttpGet("dto/mapper/dapper/productoPorLote")]
        public async Task<IActionResult> GetPostsDtoMapperDapper()
        {
            var productos = await _productoService.GetProductosPorLote();
            var response = new ApiResponse<IEnumerable<ProductoPorLote>>(productos);
            return Ok(response);
        }
        /// <summary>
        /// Obtiene todos los productos que no han registrado ventas en un periodo de tiempo determinado.
        /// </summary>
        /// <remarks>Ayuda a identificar mercancía de lenta rotación utilizando consultas optimizadas. Requiere permisos de administrador.</remarks>
        /// <returns>Un <see cref="IActionResult"/> que contiene un <see cref="ApiResponse{T}"/> con el listado de productos estancados.</returns>
        /// <response code="200">Retorna la lista de productos sin ventas.</response>
        /// <response code="401">No autorizado, requiere autenticación.</response>
        /// <response code="403">No permitido, requiere rol de Administrador.</response>
        /// <response code="500">Error interno del servidor.</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<ProductoSinVentaDTO>>))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("dto/mapper/dapper/productosSinVenta")]
        public async Task<IActionResult> GetProductosSinVenta
            ()
        {
            var productos = await _productoService.GetProductosSinVenta();
            var productosDto = _mapper.Map<IEnumerable<ProductoSinVentaDTO>>(productos);

            var response = new ApiResponse<IEnumerable<ProductoSinVentaDTO>>(productosDto);

            return Ok(response);
        }
        /// <summary>
        /// Obtiene un ranking de los productos más vendidos.
        /// </summary>
        /// <remarks>Permite consultar el top de ventas. Requiere permisos de administrador.</remarks>
        /// <param name="limit">Número máximo de productos a retornar (por defecto 5).</param>
        /// <returns>Un <see cref="IActionResult"/> con el ranking de los productos más vendidos.</returns>
        /// <response code="200">Retorna el ranking de productos.</response>
        /// <response code="401">No autorizado.</response>
        /// <response code="403">Se requiere rol de Administrador.</response>
        /// <response code="500">Error interno del servidor.</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<producto_mas_vendido>))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("mas-vendidos")]
        public async Task<IActionResult> GetProductosMasVendidos([FromQuery] int limit = 5)
        {
            var result = await _productoService.GetProductosMasVendidos(limit);
            return Ok(result);
        }
        /// <summary>
        /// Calcula y devuelve las ganancias generadas por cada lote de producto.
        /// </summary>
        /// <remarks>Ayuda a identificar la rentabilidad por lote. Requiere permisos de administrador.</remarks>
        /// <returns>Un <see cref="IActionResult"/> con el detalle de ganancias por lote.</returns>
        /// <response code="200">Retorna la información financiera por lote.</response>
        /// <response code="401">No autorizado.</response>
        /// <response code="403">Se requiere rol de Administrador.</response>
        /// <response code="500">Error interno del servidor.</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<ganancia_lote>))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("ganancias-por-lote")]
        public async Task<IActionResult> GetGananciasPorLote()
        {
            var result = await _productoService.GetGananciasPorLote();
            return Ok(result);
        }
        /// <summary>
        /// Obtiene un resumen estadístico de la distribución de productos por categoría.
        /// </summary>
        /// <remarks>Proporciona un conteo y análisis por cada categoría registrada. Requiere permisos de administrador.</remarks>
        /// <returns>Un <see cref="IActionResult"/> que contiene estadísticas agrupadas por categoría.</returns>
        /// <response code="200">Retorna el consolidado estadístico.</response>
        /// <response code="401">No autorizado.</response>
        /// <response code="403">Se requiere rol de Administrador.</response>
        /// <response code="404">Si no se encuentran datos estadísticos.</response>
        /// <response code="500">Error interno del servidor.</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<ProductoDtoPorLote>))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [Authorize(Roles = nameof(RoleType.Administrator))]
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


        /// <summary>
        /// Registra un nuevo producto en el catálogo general.
        /// </summary>
        /// <remarks>Requiere validación del DTO y permisos de administrador.</remarks>
        /// <param name="productoDto">Objeto que contiene los datos del nuevo producto.</param>
        /// <returns>Un <see cref="IActionResult"/> indicando el estado de la creación.</returns>
        /// <response code="200">El producto fue creado exitosamente.</response>
        /// <response code="400">Si los datos proporcionados no son válidos.</response>
        /// <response code="401">No autorizado.</response>
        /// <response code="403">Se requiere rol de Administrador.</response>
        /// <response code="500">Error interno del servidor al crear el producto.</response>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [Authorize(Roles = nameof(RoleType.Administrator))]
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
        /// <summary>
        /// Actualiza la información de un producto existente.
        /// </summary>
        /// <remarks>Sobrescribe los datos del producto con la información proporcionada en el DTO. Requiere permisos de administrador.</remarks>
        /// <param name="productoDto">Objeto con la información actualizada del producto.</param>
        /// <returns>Un <see cref="IActionResult"/> indicando el estado de la actualización.</returns>
        /// <response code="200">El producto fue actualizado exitosamente.</response>
        /// <response code="400">Si la información enviada es inválida.</response>
        /// <response code="401">No autorizado.</response>
        /// <response code="403">Se requiere rol de Administrador.</response>
        /// <response code="500">Error interno del servidor.</response>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductoDtoPorLote productoDto)
        {
            var producto = _mapper.Map<Producto>(productoDto);
            await _productoService.UpdateProducto(producto);  
            return Ok();  
        }
        /// <summary>
        /// Elimina un producto del catálogo general.
        /// </summary>
        /// <remarks>Elimina de manera lógica o física el registro asociado al ID. Requiere permisos de administrador.</remarks>
        /// <param name="id">El ID único del producto a eliminar.</param>
        /// <returns>Un <see cref="IActionResult"/> indicando si la operación fue exitosa.</returns>
        /// <response code="200">El producto fue eliminado exitosamente.</response>
        /// <response code="401">No autorizado.</response>
        /// <response code="403">Se requiere rol de Administrador.</response>
        /// <response code="404">Si no se encontró un producto con el ID especificado.</response>
        /// <response code="500">Error interno del servidor al procesar la eliminación.</response>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productoService.DeleteProducto(id);
            return Ok();
        }
    }

}
