using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.QueryFilter
{
    /// <summary>
    /// Objeto que contiene los parámetros opcionales para filtrar direcciones.
    /// </summary>
    public class DireccionQueryFilter
    {
        /// <summary>
        /// Filtra por nombre de la ciudad.
        /// </summary>
        public string? Ciudad { get; set; }

        /// <summary>
        /// Filtra por el nombre de la provincia o estado.
        /// </summary>
        public string? Provincia { get; set; }

        /// <summary>
        /// Filtra por el nombre del país.
        /// </summary>
        public string? Pais { get; set; }

    }
}
