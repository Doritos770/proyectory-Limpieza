using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.Auxiliares
{
    
    /// <summary>
    /// Objeto de transferencia de datos (DTO) que representa las estadísticas agrupadas por categoría de producto.
    /// </summary>
    public class EstadisticaPorCategoriaDto
    {
        /// <summary>
        /// Identificador único de la categoría.
        /// </summary>
        public int CategoriaId { get; set; }

        /// <summary>
        /// Nombre descriptivo de la categoría.
        /// </summary>
        public string CategoriaNombre { get; set; }

        /// <summary>
        /// Cantidad total de productos asociados a esta categoría.
        /// </summary>
        public int TotalProductos { get; set; }
    }
}
