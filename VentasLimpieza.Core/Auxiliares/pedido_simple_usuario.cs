namespace VentasLimpieza.Core.Auxiliares
{
    /// <summary>
    /// Clase auxiliar que contiene un resumen muy básico de un pedido asociado a un usuario.
    /// </summary>
    public class pedido_simple_usuario
    {
        /// <summary>
        /// Identificador único del pedido.
        /// </summary>
        public int PedidoId { get; set; }

        /// <summary>
        /// Estado actual del pedido (ej. Pendiente, Completado).
        /// </summary>
        public string Estado { get; set; } = null!;

        /// <summary>
        /// Monto total facturado en el pedido.
        /// </summary>
        public decimal Total { get; set; }
    }
}
