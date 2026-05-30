namespace VentasLimpieza.Core.Auxiliares
{
    /// <summary>
    /// Agrupa el historial y las estadísticas globales de los pedidos realizados por un cliente específico.
    /// </summary>
    public class pedido_usuario
    {
        /// <summary>
        /// Conteo total histórico de pedidos realizados por el usuario.
        /// </summary>
        public int CantidadPedidos { get; set; }

        /// <summary>
        /// Suma monetaria de todas las compras realizadas por este usuario.
        /// </summary>
        public decimal TotalGastado { get; set; }

        /// <summary>
        /// Colección del detalle simplificado de cada uno de los pedidos.
        /// </summary>
        public System.Collections.Generic.List<pedido_simple_usuario> Pedidos { get; set; } = new System.Collections.Generic.List<pedido_simple_usuario>();
    }
}
