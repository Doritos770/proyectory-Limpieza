using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.Dtos
{
    /// <summary>
    /// Objeto de transferencia de datos para la entidad LoteProducto.
    /// </summary>
    public class LoteproductoDto
    {
        /// <summary>
        /// Identificador único del lote.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID del producto al cual pertenece el lote.
        /// </summary>
        public int ProductoId { get; set; }

        /// <summary>
        /// Código identificador del lote.
        /// </summary>
        public string NumeroLote { get; set; } = null!;

        /// <summary>
        /// Fecha de elaboración del producto.
        /// </summary>
        public string FechaFabricacion { get; set; }

        /// <summary>
        /// Fecha de caducidad del producto.
        /// </summary>
        public string FechaCaducidad { get; set; }

        /// <summary>
        /// Unidades recibidas inicialmente.
        /// </summary>
        public int CantidadIngreso { get; set; }

        /// <summary>
        /// Unidades restantes en inventario.
        /// </summary>
        public int CantidadDisponible { get; set; }

        /// <summary>
        /// Precio de costo o compra para las unidades de este lote.
        /// </summary>
        public decimal PrecioCompra { get; set; }

        /// <summary>
        /// Especificación de la ubicación física de almacenamiento.
        /// </summary>
        public string? UbicacionAlmacen { get; set; }

        /// <summary>
        /// Bandera que indica si el lote puede ser utilizado/vendido.
        /// </summary>
        public ulong Activo { get; set; } = 1;

    }
}
