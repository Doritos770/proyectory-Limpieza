using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.Dtos
{
    /// <summary>
    /// Objeto de transferencia de datos para los códigos de seguridad (como recuperación de cuenta).
    /// </summary>
    public class CodigoseguridadDto
    {
        /// <summary>
        /// ID del usuario al que le pertenece el código.
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// El código de seguridad generado.
        /// </summary>
        public string Codigo { get; set; } = null!;

        /// <summary>
        /// Fecha y hora en la que se generó el código.
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Fecha y hora límite en la que el código puede ser utilizado.
        /// </summary>
        public DateTime FechaExpiracion { get; set; }
    }
}
