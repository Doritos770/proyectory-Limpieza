using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.Auxiliares
{
    public class ProductoPorLote
    {
        public string Categoria { get; set; }
        public int CantidadProductos { get; set; }
        public int CantidadLotes { get; set; }
        public int TotalUnidades { get; set; }
        public decimal ValorInventario { get; set; }
        
    }
}
