using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.Entities
{
    /// <summary>
    /// Entidad de dominio que representa los elementos individuales (líneas) dentro de una compra/pedido.
    /// </summary>
    public partial class Detallepedido : BaseEntity

    {
       // public int Id { get; set; }

        /// <summary>
        /// ID del pedido padre (Llave Foránea).
        /// </summary>
        public int PedidoId { get; set; }

        /// <summary>
        /// ID del producto adquirido (Llave Foránea).
        /// </summary>
        public int ProductoId { get; set; }

        /// <summary>
        /// ID del lote específico del que se tomó el producto (Llave Foránea).
        /// </summary>
        public int LoteProductoId { get; set; }

        /// <summary>
        /// Cantidad de productos de este tipo en la línea de pedido.
        /// </summary>
        public int Cantidad { get; set; }

        /// <summary>
        /// Precio cobrado por unidad al momento de la compra.
        /// </summary>
        public decimal PrecioUnitario { get; set; }

        /// <summary>
        /// Monto resultante (Cantidad * PrecioUnitario).
        /// </summary>
        public decimal Subtotal { get; set; }

        /// <summary>
        /// Navegación de EF Core al Lote.
        /// </summary>
        public virtual Loteproducto LoteProducto { get; set; } = null!;

        /// <summary>
        /// Navegación de EF Core al Pedido Padre.
        /// </summary>
        public virtual Pedido Pedido { get; set; } = null!;

        /// <summary>
        /// Navegación de EF Core al Producto.
        /// </summary>
        public virtual Producto Producto { get; set; } = null!;

    }
}