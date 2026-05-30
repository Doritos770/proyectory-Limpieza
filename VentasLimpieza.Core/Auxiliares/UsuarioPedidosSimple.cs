namespace VentasLimpieza.Core.Auxiliares
{
    /// <summary>
    /// Clase auxiliar para presentar de forma sumarizada a los usuarios junto con sus métricas de pedidos.
    /// Generalmente utilizado en paneles de administración (top clientes).
    /// </summary>
    public class UsuarioPedidosSimple
    {
        /// <summary>
        /// Identificador único del usuario.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Correo electrónico de contacto.
        /// </summary>
        public string Email { get; set; } = null!;

        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public string Nombre { get; set; } = null!;

        /// <summary>
        /// Apellido del usuario.
        /// </summary>
        public string Apellido { get; set; } = null!;

        /// <summary>
        /// Teléfono de contacto del usuario.
        /// </summary>
        public string? Telefono { get; set; }

        /// <summary>
        /// Cantidad total de pedidos que ha realizado el usuario.
        /// </summary>
        public int TotalPedidos { get; set; }

        /// <summary>
        /// Monto monetario total histórico acumulado en compras.
        /// </summary>
        public decimal TotalCompras { get; set; }
    }
}