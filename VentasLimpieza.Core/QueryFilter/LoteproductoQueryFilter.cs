using VentasLimpieza.Core.QueryFilters;

namespace VentasLimpieza.Core.QueryFilter
{
    public class LoteproductoQueryFilter : PaginationQueryFilter
    {
        public int? Id { get; set; }
        public int? ProductoId { get; set; }
        public string? NumeroLote { get; set; }
        public string? FechaFabricacion { get; set; }
        public string? FechaCaducidad { get; set; }
        public int? CantidadIngresoMin { get; set; }
        public int? CantidadIngresoMax { get; set; }
        public int? CantidadDisponibleMin { get; set; }
        public int? CantidadDisponibleMax { get; set; }
        public decimal? PrecioCompraMin { get; set; }
        public decimal? PrecioCompraMax { get; set; }
        public string? UbicacionAlmacen { get; set; }
        public ulong? Activo { get; set; }
    }
}