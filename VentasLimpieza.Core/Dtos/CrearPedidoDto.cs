using System;
using System.Collections.Generic;

namespace VentasLimpieza.core.Dtos
{
    /// <summary>
    /// Objeto de transferencia de datos utilizado para la creación (checkout) de un nuevo pedido.
    /// </summary>
    public class CrearPedidoDto
    {
        /// <summary>
        /// ID del usuario que está realizando el pedido.
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// Costo calculado aplicable por concepto de envío.
        /// </summary>
        public decimal CostoEnvio { get; set; }

        /// <summary>
        /// Medio de pago seleccionado por el usuario.
        /// </summary>
        public string MetodoPago { get; set; } = null!;

        /// <summary>
        /// Lista de los productos y cantidades a incluir en el pedido.
        /// </summary>
        public List<CrearDetallePedidoDto> Detalles { get; set; } = new List<CrearDetallePedidoDto>();
    }
}
