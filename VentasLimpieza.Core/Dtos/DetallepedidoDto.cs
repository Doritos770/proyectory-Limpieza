using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.Dtos
{
    public class DetallepedidoDto
    {
        //public int Id { get; set; }
       // public int PedidoId { get; set; }

        public int ProductoId { get; set; }

       // public int LoteProductoId { get; set; }

        public int Cantidad { get; set; }

       // public decimal PrecioUnitario { get; set; }

        public decimal Subtotal { get; set; }
    }
}
