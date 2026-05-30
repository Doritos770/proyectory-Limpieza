using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.Auxiliares
{
    
    public class EstadisticaPorCategoriaDto
    {
        public int CategoriaId { get; set; }
        public string CategoriaNombre { get; set; }
        public int TotalProductos { get; set; }
    }
}
