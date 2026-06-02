using VentasLimpieza.Core.CustomEntities;
using System.Net;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.Helpers;
using VentasLimpieza.Core.Interfaces;
using VentasLimpieza.Core.QueryFilter;
using VentasLimpieza.Services.Interfaces;
using VentasLimpieza.Services.Validators;
using VentasLimpieza.Core.Enum;
using VentasLimpieza.Core.Auxiliares;

namespace VentasLimpieza.Services.Services
{
    public class ProductoService : IProductoService
    {
        public readonly IUnitOfWork _unitOfWork;

        public ProductoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseData> GetAllProductsAsync(ProductoQueryFilter? filters = null)
        {
            var productos = await _unitOfWork.ProductoRepository.GetAll();

            if (filters != null)
            {
                if (filters.Id.HasValue)
                {
                    productos = productos.Where(p => p.Id == filters.Id.Value);
                }
                if (!string.IsNullOrEmpty(filters.Nombre))
                {
                    productos = productos.Where(p => p.Nombre.ToLower().Contains(filters.Nombre.ToLower()));
                }
                if (filters.CategoriaId.HasValue)
                {
                    productos = productos.Where(p => p.CategoriaId == filters.CategoriaId.Value);
                }
                if (!string.IsNullOrEmpty(filters.Marca))
                {
                    productos = productos.Where(p => p.Marca != null && p.Marca.ToLower().Contains(filters.Marca.ToLower()));
                }
                if (filters.PrecioMin.HasValue)
                {
                    productos = productos.Where(p => p.Precio >= filters.PrecioMin.Value);
                }
                if (filters.PrecioMax.HasValue)
                {
                    productos = productos.Where(p => p.Precio <= filters.PrecioMax.Value);
                }
                if (!string.IsNullOrEmpty(filters.FechaCreacion))
                {
                    string fechaAux = Procesos.ParseFechaFlexible(filters.FechaCreacion);
                    if (fechaAux != null)
                    {
                        DateTime fechaFiltro = Convert.ToDateTime(fechaAux);
                        productos = productos.Where(p => p.FechaCreacion.ToDateTime(TimeOnly.MinValue).Date == fechaFiltro.Date);
                    }
                }
            }
            var pagedPosts = PagedList<object>
                .Create(productos, filters.PageNumber, filters.PageSize);

            if (pagedPosts.Any())
            {
                return new ResponseData()
                {
                    Messages = new Message[] { new() { Type = TypeMessage.success.ToString(),
                        Description = "Registros de posts recuperados correctamente" } },
                    Pagination = pagedPosts,
                    StatusCode = HttpStatusCode.OK
                };
            }
            else
            {
                return new ResponseData()
                {
                    Messages = new Message[] { new() { Type = TypeMessage.warning.ToString(), Description = "No fue posible recuperar la cantidad de registros" } },
                    Pagination = pagedPosts,
                    StatusCode = HttpStatusCode.NotFound
                };
            }
            //return productos;
        }





        //public async Task<IEnumerable<Producto>> GetAllProductsAsync(ProductoQueryFilter? filters = null)
        //{
        //    var productos = await _unitOfWork.ProductoRepository.GetAll();

        //    if (filters != null)
        //    {
        //        if (filters.Id.HasValue)
        //        {
        //            productos = productos.Where(p => p.Id == filters.Id.Value);
        //        }
        //        if (!string.IsNullOrEmpty(filters.Nombre))
        //        {
        //            productos = productos.Where(p => p.Nombre.ToLower().Contains(filters.Nombre.ToLower()));
        //        }
        //        if (filters.CategoriaId.HasValue)
        //        {
        //            productos = productos.Where(p => p.CategoriaId == filters.CategoriaId.Value);
        //        }
        //        if (!string.IsNullOrEmpty(filters.Marca))
        //        {
        //            productos = productos.Where(p => p.Marca != null && p.Marca.ToLower().Contains(filters.Marca.ToLower()));
        //        }
        //        if (filters.PrecioMin.HasValue)
        //        {
        //            productos = productos.Where(p => p.Precio >= filters.PrecioMin.Value);
        //        }
        //        if (filters.PrecioMax.HasValue)
        //        {
        //            productos = productos.Where(p => p.Precio <= filters.PrecioMax.Value);
        //        }
        //        if (!string.IsNullOrEmpty(filters.FechaCreacion))
        //        {
        //            string fechaAux = Procesos.ParseFechaFlexible(filters.FechaCreacion);
        //            if (fechaAux != null)
        //            {
        //                DateTime fechaFiltro = Convert.ToDateTime(fechaAux);
        //                productos = productos.Where(p => p.FechaCreacion.ToDateTime(TimeOnly.MinValue).Date == fechaFiltro.Date);
        //            }
        //        }
        //    }
        //    var pagedPosts = PagedList<object>
        //        .Create(productos, filters.PageNumber, filters.PageSize);

        //    if (pagedPosts.Any())
        //    {
        //        return new ResponseData()
        //        {
        //            Messages = new Message[] { new() { Type = TypeMessage.success.ToString(),
        //                Description = "Registros de posts recuperados correctamente" } },
        //            Pagination = pagedPosts,
        //            StatusCode = HttpStatusCode.OK
        //        };
        //    }
        //    else
        //    {
        //        return new ResponseData()
        //        {
        //            Messages = new Message[] { new() { Type = TypeMessage.warning.ToString(), Description = "No fue posible recuperar la cantidad de registros" } },
        //            Pagination = pagedPosts,
        //            StatusCode = HttpStatusCode.NotFound
        //        };
        //    }
        //    //return productos;
        //}

        public async Task<Producto> GetProductoByIdAsync(int id)
        {
            return await _unitOfWork.ProductoRepository.GetById(id);
        }

        public async Task RegistrarProducto(Producto producto)
        {
            await ValidateNoProductoDuplicado(producto);
            await _unitOfWork.ProductoRepository.Add(producto);
            await _unitOfWork.SaveChangesAsync();
        }

        //public void UpdateProducto(Producto producto)
        //{

        //    _unitOfWork.ProductoRepository.Update(producto);
        //    _unitOfWork.SaveChanges();
        //}
        public async Task UpdateProducto(Producto productoActualizado)
        {
            var productoOriginal = await _unitOfWork.ProductoRepository.GetById(productoActualizado.Id);

            if (productoOriginal == null)
                throw new Exception("Producto no encontrado");

            if (productoOriginal.Nombre != productoActualizado.Nombre)
                throw new Exception("No se puede modificar el nombre del producto");

            if (productoOriginal.Marca != productoActualizado.Marca)
                throw new Exception("No se puede modificar la marca del producto");

            if (productoOriginal.Presentacion != productoActualizado.Presentacion)
                throw new Exception("No se puede modificar la presentacion del producto");

            if (productoActualizado.Precio <= 0)
                throw new Exception("El precio debe ser mayor a 0");

            productoOriginal.Precio = productoActualizado.Precio;

            _unitOfWork.ProductoRepository.Update(productoOriginal);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteProducto(int id)
        {
            await _unitOfWork.ProductoRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }
        /*********************************************************************************/
        public async Task<IEnumerable<Producto>> GetProductosConStockAgotado()
        {
            var productosAgotados = await _unitOfWork.ProductoRepository.GetProductosConStockAgotado();
            return productosAgotados;
        }

        public async Task<IEnumerable<producto_mas_vendido>> GetProductosMasVendidos(int limit)
        {
            return await _unitOfWork.ProductoRepository.GetProductosMasVendidos(limit);
        }

        public async Task<IEnumerable<ganancia_lote>> GetGananciasPorLote()
        {
            return await _unitOfWork.ProductoRepository.GetGananciasPorLote();
        }

        public async Task<IEnumerable<ProductoPorLote>> GetProductosPorLote()
        {
            return await _unitOfWork.ProductoRepository.GetProductosPorLote();
        }
        public async Task<IEnumerable<Producto>> GetProductosSinVenta()
        {
            return await _unitOfWork.ProductoRepository.GetProductosSinVenta();
        }
        public async Task<IEnumerable<Producto>> GetEstadisticaProductoPorCategoria()
        {
            return await _unitOfWork.ProductoRepository.GetEstadisticaProductoPorCategoria();
        }

        /*************************************************************************************/
        private async Task ValidateNoProductoDuplicado(Producto producto)
        {
            //var usuarios = await _usuarioRepository.GetAll();
            var productos = await _unitOfWork.ProductoRepository.GetAll();

            // validacion
            var existeMismoNombre = productos.Any(p =>
                p.Nombre.Equals(producto.Nombre, StringComparison.OrdinalIgnoreCase));

            if (existeMismoNombre)
                throw new Exception("Ya existe un producto registrado con este nombre");
        }
       
       
    }
}