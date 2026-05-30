using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.Auxiliares
{
    /// <summary>
    /// Representa un resumen simple de una reseña o calificación dejada por un usuario.
    /// Utilizado principalmente en consultas rápidas vía Dapper.
    /// </summary>
    public class ResenaSimple
    {
        /// <summary>
        /// Identificador único de la reseña.
        /// </summary>
        public int ResenaId { get; set; }

        /// <summary>
        /// Identificador del usuario que escribió la reseña.
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// Identificador del producto reseñado.
        /// </summary>
        public int ProductoId { get; set; }

        /// <summary>
        /// Nombre del producto reseñado, para facilitar la lectura en el frontend.
        /// </summary>
        public string ProductoNombre { get; set; }

        /// <summary>
        /// Calificación numérica otorgada por el usuario (ej. 1 al 5).
        /// </summary>
        public int Calificacion { get; set; }

        /// <summary>
        /// Texto u opinión que acompañó a la calificación.
        /// </summary>
        public string Comentario { get; set; }

        /// <summary>
        /// Fecha en que se realizó la reseña.
        /// </summary>
        public DateTime Fecha { get; set; }
    }
}
