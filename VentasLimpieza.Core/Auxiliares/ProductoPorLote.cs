using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.Auxiliares
{
    /// <summary>
    /// Clase auxiliar que agrupa información consolidada de productos basándose en sus lotes y categorías.
    /// </summary>
    public class ProductoPorLote
    {
        /// <summary>
        /// Nombre de la categoría a la que pertenecen los productos.
        /// </summary>
        public string Categoria { get; set; }

        /// <summary>
        /// Total de productos distintos dentro de la categoría.
        /// </summary>
        public int CantidadProductos { get; set; }

        /// <summary>
        /// Cantidad total de lotes registrados para esta categoría.
        /// </summary>
        public int CantidadLotes { get; set; }

        /// <summary>
        /// Sumatoria total de unidades disponibles en todos los lotes de la categoría.
        /// </summary>
        public int TotalUnidades { get; set; }

        /// <summary>
        /// Valor financiero total estimado del inventario actual para la categoría.
        /// </summary>
        public decimal ValorInventario { get; set; }
        
    }
}
