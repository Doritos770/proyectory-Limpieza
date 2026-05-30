using System;

namespace VentasLimpieza.core.Dtos
{
    /// <summary>
    /// Objeto de transferencia de datos utilizado al momento de crear las líneas de un nuevo pedido.
    /// </summary>
    public class CrearDetallePedidoDto
    {
        /// <summary>
        /// Identificador del producto a comprar.
        /// </summary>
        public int ProductoId { get; set; }

        /// <summary>
        /// Cantidad de unidades a adquirir de este producto.
        /// </summary>
        public int Cantidad { get; set; }
    }
}
