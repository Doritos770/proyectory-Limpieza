using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.QueryFilter
{
    /// <summary>
    /// Objeto que contiene los parámetros opcionales para filtrar códigos de seguridad.
    /// </summary>
    public class CodigoseguridadQueryFilter
    {
        /// <summary>
        /// Filtra por el ID del registro del código de seguridad.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Filtra por el ID del usuario propietario del código.
        /// </summary>
        public int? UsuarioId { get; set; }

        /// <summary>
        /// Filtra por el valor literal del código.
        /// </summary>
        public string? Codigo { get; set; }

        /// <summary>
        /// Filtra por la fecha en que se creó el código.
        /// </summary>
        public string? FechaCreacion { get; set; }

        /// <summary>
        /// Filtra por la fecha en la que el código expira.
        /// </summary>
        public string? FechaExpiracion { get; set; }

        /// <summary>
        /// Filtra según el estado de uso del código (1 usado, 0 no usado).
        /// </summary>
        public ulong? Usado { get; set; }
    }
}
