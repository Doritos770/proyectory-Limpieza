using System;
using System.Collections.Generic;
using System.Text;
using VentasLimpieza.Core.QueryFilters;

namespace VentasLimpieza.Core.QueryFilter
{
    public class UsuarioQueryFilter : PaginationQueryFilter
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Telefono { get; set; }
        public string? FechaRegistro { get; set; }
    }
}
