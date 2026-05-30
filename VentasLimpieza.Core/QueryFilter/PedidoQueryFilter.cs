using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.QueryFilter
{
    /// <summary>
    /// Objeto que contiene los parámetros opcionales para filtrar pedidos.
    /// </summary>
    public class PedidoQueryFilter
    {
        /// <summary>
        /// Filtra los pedidos realizados por un ID de usuario específico.
        /// </summary>
        public int? UsuarioId { get; set; }

        /// <summary>
        /// Filtra por la fecha en que se realizó el pedido.
        /// </summary>
        public DateTime? FechaPedido { get; set; }

        /// <summary>
        /// Filtra por el costo asociado al envío.
        /// </summary>
        public decimal? CostoEnvio { get; set; }

        /// <summary>
        /// Filtra por la cantidad de productos en el pedido.
        /// </summary>
        public int? CantidadProducto { get; set; }

        /// <summary>
        /// Filtra por el monto total facturado.
        /// </summary>
        public decimal? Total { get; set; }

    }
}
