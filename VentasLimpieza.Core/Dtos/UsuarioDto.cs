using System;
using System.Collections.Generic;

namespace VentasLimpieza.core.Dtos;

public partial class UsuarioDto
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string? Telefono { get; set; }

    public DateOnly FechaRegistro { get; set; }

    public ulong IsActive { get; set; }
}
