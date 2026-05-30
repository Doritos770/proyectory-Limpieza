namespace VentasLimpieza.Core.QueryFilter
{
    public class DetallepedidoQueryFilter
    {
        public int? Id { get; set; }
        public int? PedidoId { get; set; }
        public int? ProductoId { get; set; }
        public int? LoteProductoId { get; set; }
        public int? Cantidad { get; set; }
        public decimal? PrecioUnitarioMin { get; set; }
        public decimal? PrecioUnitarioMax { get; set; }
        public decimal? SubtotalMin { get; set; }
        public decimal? SubtotalMax { get; set; }
    }
}