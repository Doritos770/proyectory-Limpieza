namespace VentasLimpieza.Core.Entities
{

    /// <summary>
    /// Entidad de dominio que representa las transacciones de compra/pedidos realizados.
    /// </summary>
    public partial class Pedido : BaseEntity
    {
       // public int Id { get; set; }

        /// <summary>
        /// ID del usuario que registró la compra (Llave Foránea).
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// Fecha y hora en la cual se completó el registro del pedido.
        /// </summary>
        public DateTime FechaPedido { get; set; }

        /// <summary>
        /// Estado actual del pedido (ej. Procesando, Enviado, Entregado).
        /// </summary>
        public string Estado { get; set; } = null!;

        /// <summary>
        /// Monto sumado por concepto de logística y entrega.
        /// </summary>
        public decimal CostoEnvio { get; set; }

        /// <summary>
        /// Método de pago seleccionado (ej. Tarjeta de crédito, Paypal, Transferencia).
        /// </summary>
        public string MetodoPago { get; set; } = null!;

        /// <summary>
        /// Monto final total a pagar (Subtotal + CostoEnvio).
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// Colección de los detalles/líneas individuales de este pedido.
        /// </summary>
        public virtual ICollection<Detallepedido> Detallepedidos { get; set; } = new List<Detallepedido>();

        /// <summary>
        /// Propiedad de navegación de EF Core hacia el Usuario comprador.
        /// </summary>
        public virtual Usuario Usuario { get; set; } = null!;
    }

}