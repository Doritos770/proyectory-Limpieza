using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.Entities
{
    /// <summary>
    /// Clase abstracta base que proporciona propiedades comunes para las entidades del dominio.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Identificador principal (Primary Key) de la entidad.
        /// </summary>
        public int Id { get; set; }
        // public DateTime CreatedDate { get; set; }

    }
}
