using VentasLimpieza.Core.QueryFilters;

namespace VentasLimpieza.Core.QueryFilter
{
    /// <summary>
    /// Objeto que contiene los parámetros opcionales (incluyendo paginación) para filtrar lotes de productos.
    /// </summary>
    public class LoteproductoQueryFilter : PaginationQueryFilter
    {
        /// <summary>
        /// Filtra por el ID del lote.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Filtra por el ID del producto que pertenece al lote.
        /// </summary>
        public int? ProductoId { get; set; }

        /// <summary>
        /// Filtra de forma parcial o exacta por el número de lote.
        /// </summary>
        public string? NumeroLote { get; set; }

        /// <summary>
        /// Filtra por la fecha de fabricación.
        /// </summary>
        public string? FechaFabricacion { get; set; }

        /// <summary>
        /// Filtra por la fecha de caducidad.
        /// </summary>
        public string? FechaCaducidad { get; set; }

        /// <summary>
        /// Rango de búsqueda: cantidad inicial ingresada (mínimo).
        /// </summary>
        public int? CantidadIngresoMin { get; set; }

        /// <summary>
        /// Rango de búsqueda: cantidad inicial ingresada (máximo).
        /// </summary>
        public int? CantidadIngresoMax { get; set; }

        /// <summary>
        /// Rango de búsqueda: stock disponible actual (mínimo).
        /// </summary>
        public int? CantidadDisponibleMin { get; set; }

        /// <summary>
        /// Rango de búsqueda: stock disponible actual (máximo).
        /// </summary>
        public int? CantidadDisponibleMax { get; set; }

        /// <summary>
        /// Rango de búsqueda: costo unitario mínimo.
        /// </summary>
        public decimal? PrecioCompraMin { get; set; }

        /// <summary>
        /// Rango de búsqueda: costo unitario máximo.
        /// </summary>
        public decimal? PrecioCompraMax { get; set; }

        /// <summary>
        /// Filtra por la ubicación física dentro del almacén.
        /// </summary>
        public string? UbicacionAlmacen { get; set; }

        /// <summary>
        /// Filtra por el estado activo o inactivo del lote.
        /// </summary>
        public ulong? Activo { get; set; }
    }
}