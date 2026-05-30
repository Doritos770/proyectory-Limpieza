using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.Dtos
{
    /// <summary>
    /// Objeto de transferencia de datos para la entidad Detalle de Pedido.
    /// </summary>
    public class DetallepedidoDto
    {
        //public int Id { get; set; }
        // public int PedidoId { get; set; }

        /// <summary>
        /// ID del producto adquirido en el detalle del pedido.
        /// </summary>
        public int ProductoId { get; set; }

        // public int LoteProductoId { get; set; }

        /// <summary>
        /// Cantidad de unidades adquiridas.
        /// </summary>
        public int Cantidad { get; set; }

        // public decimal PrecioUnitario { get; set; }

        /// <summary>
        /// Monto subtotal calculado (cantidad * precio unitario).
        /// </summary>
        public decimal Subtotal { get; set; }
    }
}
