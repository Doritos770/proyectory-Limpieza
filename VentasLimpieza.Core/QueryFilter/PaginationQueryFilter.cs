using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentasLimpieza.Core.QueryFilters
{
    /// <summary>
    /// Objeto base que define los parámetros estándar requeridos para paginar un listado.
    /// </summary>
    public class PaginationQueryFilter
    {
        /// <summary>
        /// Tamaño de la página o cantidad máxima de elementos a retornar.
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Número de la página solicitada.
        /// </summary>
        public int PageNumber { get; set; }
    }
}
