using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.Entities
{
    public partial class Detallepedido : BaseEntity

    {
       // public int Id { get; set; }

        public int PedidoId { get; set; }

        public int ProductoId { get; set; }

        public int LoteProductoId { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal { get; set; }

        public virtual Loteproducto LoteProducto { get; set; } = null!;

        public virtual Pedido Pedido { get; set; } = null!;

        public virtual Producto Producto { get; set; } = null!;

    }
}