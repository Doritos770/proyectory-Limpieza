using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Infrastructure.Queries
{
    public class slqCodigoseguridad
    {
        public static string UltimoCodigo = @"
            SELECT Codigo
            FROM CodigoSeguridad
            WHERE UsuarioId = @UsuarioId
            ORDER BY FechaCreacion DESC
            LIMIT 1;";
    }
}
