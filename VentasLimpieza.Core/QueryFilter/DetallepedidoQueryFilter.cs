namespace VentasLimpieza.Core.QueryFilter
{
    /// <summary>
    /// Objeto que contiene los parámetros opcionales para filtrar detalles de pedidos.
    /// </summary>
    public class DetallepedidoQueryFilter
    {
        /// <summary>
        /// Filtra por el ID del detalle del pedido.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Filtra por el ID del pedido padre.
        /// </summary>
        public int? PedidoId { get; set; }

        /// <summary>
        /// Filtra por el ID del producto comprado.
        /// </summary>
        public int? ProductoId { get; set; }

        /// <summary>
        /// Filtra por el ID del lote desde el cual se despachó el producto.
        /// </summary>
        public int? LoteProductoId { get; set; }

        /// <summary>
        /// Filtra por la cantidad comprada.
        /// </summary>
        public int? Cantidad { get; set; }

        /// <summary>
        /// Rango de búsqueda: precio unitario mínimo.
        /// </summary>
        public decimal? PrecioUnitarioMin { get; set; }

        /// <summary>
        /// Rango de búsqueda: precio unitario máximo.
        /// </summary>
        public decimal? PrecioUnitarioMax { get; set; }

        /// <summary>
        /// Rango de búsqueda: subtotal mínimo.
        /// </summary>
        public decimal? SubtotalMin { get; set; }

        /// <summary>
        /// Rango de búsqueda: subtotal máximo.
        /// </summary>
        public decimal? SubtotalMax { get; set; }
    }
}