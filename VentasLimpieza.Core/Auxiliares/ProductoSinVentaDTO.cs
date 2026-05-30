using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.Auxiliares
{
    public class ProductoSinVentaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public string Presentacion { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Categoria { get; set; }
        public int StockTotal { get; set; }
        public int CantidadLotes { get; set; }
        public int DiasDesdeCreacion { get; set; }
    }
}
