using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.Dtos
{
    public class CodigoseguridadDto
    {
        public int UsuarioId { get; set; }

        public string Codigo { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaExpiracion { get; set; }
    }
}
