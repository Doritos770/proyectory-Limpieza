using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.CustomEntities
{
    /// <summary>
    /// Estructura estándar para devolver detalles de errores desde la API.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Código de estado HTTP del error.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Título o resumen breve del error.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Descripción detallada del error.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Objeto opcional que contiene errores de validación específicos por campo.
        /// </summary>
        public object? Errors { get; set; }

        /// <summary>
        /// Identificador de traza único para seguimiento en logs.
        /// </summary>
        public string? TraceId { get; set; }
    }
}
