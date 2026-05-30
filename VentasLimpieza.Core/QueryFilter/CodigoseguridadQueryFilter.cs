using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.QueryFilter
{
    public class CodigoseguridadQueryFilter
    {
        public int? Id { get; set; }
        public int? UsuarioId { get; set; }
        public string? Codigo { get; set; }
        public string? FechaCreacion { get; set; }
        public string? FechaExpiracion { get; set; }
        public ulong? Usado { get; set; }
    }
}
