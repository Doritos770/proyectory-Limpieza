using Swashbuckle.AspNetCore.Annotations;
using VentasLimpieza.Core.QueryFilters;

namespace VentasLimpieza.Core.QueryFilter
{
    /// <summary>
    /// Objeto que contiene los parámetros opcionales (incluyendo paginación) para filtrar el catálogo de productos.
    /// </summary>
    public class ProductoQueryFilter : PaginationQueryFilter
    {
        /// <summary>
        /// Filtra por el identificador del producto.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Búsqueda parcial por el nombre del producto.
        /// </summary>
        public string? Nombre { get; set; }

        /// <summary>
        /// Filtra productos pertenecientes a una categoría específica.
        /// </summary>
        public int? CategoriaId { get; set; }

        /// <summary>
        /// Búsqueda parcial por la marca del producto.
        /// </summary>
        public string? Marca { get; set; }

        /// <summary>
        /// Rango de búsqueda: Precio mínimo.
        /// </summary>
        public decimal? PrecioMin { get; set; }

        /// <summary>
        /// Rango de búsqueda: Precio máximo.
        /// </summary>
        public decimal? PrecioMax { get; set; }

        /// <summary>
        /// Filtra por la fecha de creación del registro.
        /// </summary>
        public string? FechaCreacion { get; set; }
    }
}
