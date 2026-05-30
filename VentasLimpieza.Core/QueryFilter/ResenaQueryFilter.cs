using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.QueryFilter
{
    /// <summary>
    /// Objeto que contiene los parámetros opcionales para filtrar reseñas de productos.
    /// </summary>
    public class ResenaQueryFilter
    {
        /// <summary>
        /// Filtra por la calificación numérica otorgada (ej. 5 estrellas).
        /// </summary>
        public int? Calificacion { get; set; }

        /// <summary>
        /// Búsqueda parcial dentro del texto del comentario de la reseña.
        /// </summary>
        public string? Comentario { get; set; }
    }
}
