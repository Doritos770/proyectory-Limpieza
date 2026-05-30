using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.QueryFilter
{
    public class PedidoQueryFilter
    {
        public int? UsuarioId { get; set; }
        public DateTime? FechaPedido { get; set; }
        public decimal? CostoEnvio { get; set; }
        public int? CantidadProducto { get; set; }
        public decimal? Total { get; set; }

    }
}
