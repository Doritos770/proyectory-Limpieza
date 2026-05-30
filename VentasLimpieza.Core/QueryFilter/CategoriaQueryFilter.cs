using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.QueryFilter
{
    /// <summary>
    /// Objeto que contiene los parámetros opcionales para filtrar la búsqueda de categorías.
    /// </summary>
    public class CategoriaQueryFilter
    {
        /// <summary>
        /// Filtra por el ID de un usuario específico.
        /// </summary>
        public int? UsuarioId { get; set; }

        /// <summary>
        /// Filtra por el ID de un producto en específico.
        /// </summary>
        public int? ProductoId { get; set; }

        /// <summary>
        /// Filtra según la calificación obtenida.
        /// </summary>
        public int?  Calificacion { get; set; }

    }
}
