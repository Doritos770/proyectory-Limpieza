using Swashbuckle.AspNetCore.Annotations;
using VentasLimpieza.Core.QueryFilters;

namespace VentasLimpieza.Core.QueryFilter
{
    public class ProductoQueryFilter : PaginationQueryFilter
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public int? CategoriaId { get; set; }
        public string? Marca { get; set; }
        public decimal? PrecioMin { get; set; }
        public decimal? PrecioMax { get; set; }
        public string? FechaCreacion { get; set; }
    }
}
