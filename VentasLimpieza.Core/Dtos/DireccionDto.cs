using System;
using System.Collections.Generic;

namespace VentasLimpieza.core.Dtos;

public partial class DireccionDto
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public string Direccion1 { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public string Provincia { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public ulong Principal { get; set; }
    
}
