using System;
using System.Collections.Generic;
using System.Text;
using VentasLimpieza.Core.QueryFilters;

namespace VentasLimpieza.Core.QueryFilter
{
    /// <summary>
    /// Objeto que contiene los parámetros opcionales (incluyendo paginación) para filtrar usuarios.
    /// </summary>
    public class UsuarioQueryFilter : PaginationQueryFilter
    {
        /// <summary>
        /// Filtra de forma parcial o exacta por el email del usuario.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Filtro de contraseña (Normalmente evitado por seguridad, mantenido por requerimientos base).
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Búsqueda parcial por el nombre del usuario.
        /// </summary>
        public string? Nombre { get; set; }

        /// <summary>
        /// Búsqueda parcial por el apellido del usuario.
        /// </summary>
        public string? Apellido { get; set; }

        /// <summary>
        /// Búsqueda parcial por el número telefónico del usuario.
        /// </summary>
        public string? Telefono { get; set; }

        /// <summary>
        /// Filtra por la fecha de registro en el sistema.
        /// </summary>
        public string? FechaRegistro { get; set; }
    }
}
